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
    public class QuizController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public QuizController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        [HttpPost]
        [Authorize(Roles =UserRoles.Teacher)]
        public async Task<IActionResult> AddQuiz([FromBody]AddQuizRequest model)
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
            await _context.Quizzes.AddAsync(new Quiz { Id = Guid.NewGuid(), Title = model.Title, Author = user });
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Teacher)]
        public async Task<IActionResult> GetQuizzes()
        {

            var userId = this.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            return Ok(await _context.Quizzes.Include(quiz => quiz.Tests).Where(quiz => quiz.Author.Id == userId)
                                            .Select(quiz => new GetQuizzesResponse { QuizId = quiz.Id, QuizTitle = quiz.Title, Tests = quiz.Tests.Select(test => new TestsIndexDto { TestId = test.Id, TestTitle = test.Title }).ToList() })
                                            .AsNoTracking().ToListAsync());
        }


        [HttpPost("{id}/questions")]
        [Authorize(Roles = UserRoles.Teacher)]
        public async Task<IActionResult> AddQuestions(Guid id,[FromBody] AddQuizQuestionsRequest model)
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

            var quiz = await _context.Quizzes.Include(quiz => quiz.Author).Include(quiz => quiz.Questions).ThenInclude(question=> question.Options).FirstOrDefaultAsync(quiz => quiz.Id == id);
            if(quiz == null || quiz.Author.Id != userId)
            {
                return BadRequest();
            }

            quiz.Questions.Clear();

            foreach(var modelQuestion in model.Questions)
            {
                var newQuestion = new Question { Id = Guid.NewGuid(), QuestionText = modelQuestion.QuestionText, Difficulty = modelQuestion.Difficulty, Type = modelQuestion.Type, Quiz = quiz };
                await _context.Questions.AddAsync(newQuestion);

                if (modelQuestion.Options != null)
                {
                    foreach (var option in modelQuestion.Options)
                    {
                        var newOption = new Option { Id = Guid.NewGuid(), OptionText = option.OptionText, IsCorrect = option.IsCorrect, Question = newQuestion };
                        await _context.Options.AddAsync(newOption);
                    }

                }
                
            }

            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpGet("{id}/questions")]
        [Authorize(Roles = UserRoles.Teacher)]
        public async Task<IActionResult> GetQuestions(Guid id)
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

            var quiz = await _context.Quizzes.Include(quiz => quiz.Author).Include(quiz => quiz.Questions).ThenInclude(question => question.Options).AsNoTracking().FirstOrDefaultAsync(quiz => quiz.Id == id);
            if (quiz == null || quiz.Author.Id != userId)
            {
                return BadRequest();
            }



            return Ok(quiz.Questions.Select(question =>
                                                new QuestionGetDto
                                                {
                                                    Id = question.Id,
                                                    QuestionText = question.QuestionText,
                                                    Difficulty = question.Difficulty,
                                                    Type = question.Type,
                                                    Options = question.Options.Select(option =>
                                                    new OptionGetDto { Id = option.Id,OptionText = option.OptionText, IsCorrect = option.IsCorrect }).ToList()
                                                }).ToList());
        }

    }
}
