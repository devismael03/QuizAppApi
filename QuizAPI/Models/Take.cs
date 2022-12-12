using QuizAPI.Authentication;

namespace QuizAPI.Models
{
    public class Take
    {
        public Guid Id { get; set; }
        public Guid AnonymousCheckIdentifier { get; set; }
        public double Score { get; set; }
        public double SuccessRate { get; set; }
        public ApplicationUser Taker { get; set; }
        public Test Test { get; set; }
        public List<TakeQuestion> TakeQuestions { get; set; }
    }
}
