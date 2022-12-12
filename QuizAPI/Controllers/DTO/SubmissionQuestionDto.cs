using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Controllers.DTO
{
    public class SubmissionQuestionDto
    {
        [Required]
        public string QuestionId { get; set; }
        public string? OpenEndedAnswer { get; set; }
        public List<string>? Answers { get; set; } //option id-s should be sent
    }
}
