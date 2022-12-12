using QuizAPI.Models;

namespace QuizAPI.Controllers.DTO
{
    public class QuestionGetDto
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public QuizType Type { get; set; }

        public List<OptionGetDto>? Options { get; set; }

    }
}
