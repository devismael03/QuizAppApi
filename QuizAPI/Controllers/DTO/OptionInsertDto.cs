using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Controllers.DTO
{
    public class OptionInsertDto
    {
        public string OptionText { get; set; }

        [Required]
        public bool IsCorrect { get; set; }
    }
}
