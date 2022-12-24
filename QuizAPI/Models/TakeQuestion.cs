namespace QuizAPI.Models
{
    public class TakeQuestion
    {
        public Guid Id { get; set; }
        public string? OpenEndedAnswer { get; set; }
        public double Score { get; set; }
        public bool IsOpenEndedQuestionChecked { get; set; }
        public Take Take { get; set; }
        public Question Question { get; set; }
        public List<TakeAnswer> TakeAnswers { get; set; }
    }
}
