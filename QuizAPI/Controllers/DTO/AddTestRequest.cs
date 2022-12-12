using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Controllers.DTO
{
    public class AddTestRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string QuizId { get; set; }

        [Required]
        public int EasyQuestionCount { get; set; }

        [Required]
        public int MediumQuestionCount { get; set; }

        [Required]
        public int HardQuestionCount { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

    }
}
