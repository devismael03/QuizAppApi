using QuizAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Controllers.DTO
{
    public class QuestionInsertDto
    {
        public string QuestionText { get; set; }
        
        [Required]
        public DifficultyLevel Difficulty { get; set; }

        [Required]
        public QuizType Type { get; set; }
        public List<OptionInsertDto>? Options { get; set; }
    }
}
