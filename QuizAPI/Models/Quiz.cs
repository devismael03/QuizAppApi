using QuizAPI.Authentication;

namespace QuizAPI.Models
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ApplicationUser Author { get; set; }
        public List<Question> Questions { get; set; }
        public List<Test> Tests { get; set; }
    }
}
