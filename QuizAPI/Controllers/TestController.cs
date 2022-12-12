using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Authentication;
using QuizAPI.Controllers.DTO;
using QuizAPI.Models;
using System.Text;

namespace QuizAPI.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TestController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        [HttpPost]
        [Authorize(Roles =UserRoles.Teacher)]
        public async Task<IActionResult> AddTest([FromBody]AddTestRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quiz = await _context.Quizzes.FirstOrDefaultAsync(quiz => quiz.Id == Guid.Parse(model.QuizId));

            if(quiz == null)
            {
                return BadRequest();
            }

            await _context.Tests.AddAsync(new Test { Id = Guid.NewGuid(), Title = model.Title, StartDate = model.StartDate, EndDate = model.EndDate, EasyQuestionCount = model.EasyQuestionCount, MediumQuestionCount = model.MediumQuestionCount, HardQuestionCount = model.HardQuestionCount, Quiz= quiz});
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize(Roles =UserRoles.Student)]
        public async Task<IActionResult> GetTestQuestions(Guid id)
        {
            var test = await _context.Tests.Include(test => test.Quiz).ThenInclude(quiz => quiz.Questions).ThenInclude(question => question.Options).FirstOrDefaultAsync(test => test.Id == id);

            if (test == null)
            {
                return BadRequest();
            }

            if(test.StartDate > DateTime.UtcNow)
            {
                return StatusCode(403, new { Message = "Not started yet"});
            }

            if (test.EndDate < DateTime.UtcNow)
            {
                return StatusCode(403, new { Message = "Quiz is over" });
            }

            List<Question> easyQuestions = test.Quiz.Questions.Where(quiz => quiz.Difficulty == DifficultyLevel.Easy).OrderBy(question => Guid.NewGuid()).Take(test.EasyQuestionCount).ToList();
            List<Question> mediumQuestions = test.Quiz.Questions.Where(quiz => quiz.Difficulty == DifficultyLevel.Medium).OrderBy(question => Guid.NewGuid()).Take(test.MediumQuestionCount).ToList();
            List<Question> hardQuestions = test.Quiz.Questions.Where(quiz => quiz.Difficulty == DifficultyLevel.Hard).OrderBy(question => Guid.NewGuid()).Take(test.HardQuestionCount).ToList();

            List<Question> finalQuestions = new List<Question>(easyQuestions);
            finalQuestions.AddRange(mediumQuestions);
            finalQuestions.AddRange(hardQuestions);
            return Ok(new
            {
                Id = test.Id,
                StartDate = test.StartDate,
                EndDate = test.EndDate,
                Questions = finalQuestions.Select(question =>
                                                new QuestionGetDto
                                                {
                                                    Id = question.Id,
                                                    QuestionText = question.QuestionText,
                                                    Difficulty = question.Difficulty,
                                                    Type = question.Type,
                                                    Options = question.Options.Select(option =>
                                                    new OptionGetDto { Id = option.Id,OptionText = option.OptionText}).ToList()
                                                })
            });
        }


        [HttpPost("submit")]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<IActionResult> SubmitTest([FromBody]TestSubmissionRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = this.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);

            var previousTake = await _context.Takes.FirstOrDefaultAsync(take => take.Test.Id == Guid.Parse(model.TestId) && take.Taker.Id == userId);
            
            if(previousTake != null)
            {
                return StatusCode(403, new { Message = "You have already submit your answers to this test!" });
            }


            var test = await _context.Tests.Include(test => test.Takes).Include(test => test.Quiz).FirstOrDefaultAsync(test => test.Id == Guid.Parse(model.TestId));

            if (test == null)
            {
                return BadRequest();
            }

            if (test.StartDate > DateTime.UtcNow)
            {
                return StatusCode(403, new { Message = "Not started yet" });
            }

            if (test.EndDate.AddMinutes(3) < DateTime.UtcNow) //DOING this for late automatic submission by web app frontend after the clock is over
            {
                return StatusCode(403, new { Message = "Quiz is over" });
            }

            var newTake = new Take { Id= Guid.NewGuid(), AnonymousCheckIdentifier = Guid.NewGuid(), Score = 0 , SuccessRate = 0, Taker = user, Test = test };


            var totalScore = 0;

            List<string> alreadyCheckedQuestions = new List<string>();

            foreach(var answeredQuestion in model.Questions)
            {
                var questionInDb = await _context.Questions.Include(question => question.Options).FirstOrDefaultAsync(question => question.Id == Guid.Parse(answeredQuestion.QuestionId) && question.Quiz.Id == test.Quiz.Id);
                if(questionInDb != null)
                {
                    if (alreadyCheckedQuestions.Contains(questionInDb.Id.ToString()))
                    {
                        continue;
                    }
                    alreadyCheckedQuestions.Add(questionInDb.Id.ToString());
                    var takeQuestion = new TakeQuestion { Id = Guid.NewGuid(), OpenEndedAnswer = answeredQuestion.OpenEndedAnswer, Question = questionInDb, Take = newTake ,Score = 0};
                    if(answeredQuestion.Answers != null && questionInDb.Type != QuizType.OpenEnded)
                    {
                        if (questionInDb.Type == QuizType.SingleChoice && answeredQuestion.Answers.Count > 1) //for single choice, if someone wants to post multiple answers, we skip checking this question
                            continue;

                        var correctAnswerCountInDb = questionInDb.Options.Count(option => option.IsCorrect);
                        var currentCorrectAnswerCount = 0;

                        foreach (var answer in answeredQuestion.Answers)
                        {
                            var optionInDb = await _context.Options.FirstOrDefaultAsync(option => option.Id == Guid.Parse(answer) && option.Question.Id == questionInDb.Id);
                            if(optionInDb != null)
                            {
                                if (optionInDb.IsCorrect)
                                {
                                    currentCorrectAnswerCount++;
                                }
                                await _context.TakeAnswers.AddAsync(new TakeAnswer { Id = Guid.NewGuid(), Option = optionInDb, TakeQuestion = takeQuestion});
                            }
                        }

                        if (currentCorrectAnswerCount == correctAnswerCountInDb && answeredQuestion.Answers.Count == correctAnswerCountInDb)
                        {
                            takeQuestion.Score = 1;
                            totalScore++;
                        }
                    }
                    
                    await _context.TakeQuestion.AddAsync(takeQuestion);
                }
            }

            newTake.Score = totalScore;
            newTake.SuccessRate = (totalScore / (double)(test.EasyQuestionCount + test.MediumQuestionCount + test.HardQuestionCount)) * 100;
            await _context.Takes.AddAsync(newTake);
            await _context.SaveChangesAsync();

            return Ok();
        }



        [HttpGet("{id}/takes")]
        [Authorize(Roles = UserRoles.Teacher)]
        public async Task<IActionResult> GetTakes(Guid id,bool anonymous)
        {
            var userId = this.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }


            var test = await _context.Tests.Include(test => test.Takes).ThenInclude(take => take.Taker).FirstOrDefaultAsync(test => test.Id == id && test.Quiz.Author.Id == userId);

            if (test == null)
            {
                return BadRequest();
            }

            

            if (anonymous)
            {
                return Ok(test.Takes.Select(take => take.AnonymousCheckIdentifier).ToList());
            }
            else
            {
                return Ok(test.Takes.Select(take => new { Id = take.Id, Score = take.Score, SuccessRate = take.SuccessRate, FullName = take.Taker.FirstName + " " + take.Taker.LastName }));
            }

        }

    }
}
