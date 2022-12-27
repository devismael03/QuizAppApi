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
    public class TakeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public TakeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        [Authorize(Roles =UserRoles.Teacher)]
        [HttpGet("{anonymousId}/openended")]
        public async Task<IActionResult> GetOpenEndedAnswers(Guid anonymousId)
        {
            var userId = this.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var take = await _context.Takes.Include(take=>take.TakeQuestions).ThenInclude(takequestion => takequestion.Question).AsNoTracking().FirstOrDefaultAsync(take => take.AnonymousCheckIdentifier == anonymousId && take.Test.Quiz.Author.Id == userId);
            if(take == null)
            {
                return BadRequest();
            }

            return Ok(take.TakeQuestions.Where(takequestion => takequestion.Question.Type == QuizType.OpenEnded)
                                        .Select(takequestion => new { TakeQuestionId = takequestion.Id, QuestionText = takequestion.Question.QuestionText, StudentAnswer = takequestion.OpenEndedAnswer , Score = takequestion.Score * 10})
                                        .ToList());
        
        }

        [Authorize(Roles = UserRoles.Teacher)]
        [HttpPost("givemark/{anonymousId}")]
        public async Task<IActionResult> GiveMark(Guid anonymousId,[FromBody]GiveMarkRequest model)
        {
            var userId = this.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var take = await _context.Takes.Include(take => take.Test).Include(take => take.TakeQuestions).ThenInclude(takequestion => takequestion.Question).FirstOrDefaultAsync(take => take.AnonymousCheckIdentifier == anonymousId && take.Test.Quiz.Author.Id == userId);
            if (take == null)
            {
                return BadRequest();
            }

            foreach(var mark in model.Marks)
            {
                var takeQuestion = take.TakeQuestions.Where(takequestion => takequestion.Id == Guid.Parse(mark.Id)).FirstOrDefault();
                if(takeQuestion != null)
                {
                    take.Score -= takeQuestion.Score; //if the process is updating the mark, then we need to subtract previous mark from total score
                    takeQuestion.Score = mark.Score / 10.0;
                    take.Score += takeQuestion.Score;
                    takeQuestion.IsOpenEndedQuestionChecked = true;
                }
            }

            take.SuccessRate = (take.Score / (double)(take.Test.EasyQuestionCount + take.Test.MediumQuestionCount + take.Test.HardQuestionCount)) * 100;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Authorize(Roles = $"{UserRoles.Teacher},{UserRoles.Student}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTakeResults(Guid id)
        {
            var userId = this.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var take = await _context.Takes.Include(take => take.Taker)
                .Include(take => take.Test)
                .Include(take => take.TakeQuestions)
                .ThenInclude(takequestion => takequestion.Question)
                .ThenInclude(question => question.Options)
                .Include(take => take.TakeQuestions)
                .ThenInclude(takequestion => takequestion.TakeAnswers)
                .ThenInclude(takeanswer => takeanswer.Option)
                .AsNoTracking().FirstOrDefaultAsync(take => take.Id == id && (take.Test.Quiz.Author.Id == userId || take.Taker.Id == userId));
            if (take == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                TestTitle = take.Test.Title,
                Score = take.Score,
                SuccessRate = take.SuccessRate,
                FullName = take.Taker.FirstName + " " + take.Taker.LastName,
                Answers = take.TakeQuestions.Select(takequestion => new {
                    Text = takequestion.Question.QuestionText,
                    Type = takequestion.Question.Type,
                    Difficulty = takequestion.Question.Difficulty,
                    Score = takequestion.Score,
                    OpenEndedAnswer = takequestion.OpenEndedAnswer ?? String.Empty,
                    Options = takequestion.Question.Options.Select(option => new
                    {
                        Text = option.OptionText,
                        IsCorrect = option.IsCorrect,
                        IsSelectedByTaker = takequestion.TakeAnswers.Exists(takeanswer => takeanswer.Option.Id == option.Id)
                    })
                })
            }
            );

        }


        [HttpGet]
        [Authorize(Roles = UserRoles.Student)]
        public async Task<IActionResult> GetTakesOfStudent()
        {
            var userId = this.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            return Ok(await _context.Takes.Where(take => take.Taker.Id == userId).Select(take => new { Id = take.Id, Score = take.Score, SuccessRate = take.SuccessRate, TestTitle = take.Test.Title}).AsNoTracking().ToListAsync());
        }

    }
}
