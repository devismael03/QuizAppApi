using System.ComponentModel.DataAnnotations;

namespace QuizAPI.Controllers.DTO
{
    public class AddQuizRequest
    {
        [Required]
        public string Title { get; set; }
    }
}
