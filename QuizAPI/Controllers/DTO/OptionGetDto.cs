
namespace QuizAPI.Controllers.DTO
{
    public class OptionGetDto
    {
        public Guid Id { get; set; }
        public string OptionText { get; set; }

        public bool? IsCorrect { get; set; } = null;
    }
}
