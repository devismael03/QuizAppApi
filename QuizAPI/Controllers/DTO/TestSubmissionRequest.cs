using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Controllers.DTO
{
    public class TestSubmissionRequest
    {
        [Required]
        public string TestId { get; set; }

        
        public List<SubmissionQuestionDto> Questions { get; set; }
    }
}
