namespace QuizAPI.Models
{
    public class Option
    {
        public Guid Id { get; set; }
        public string OptionText { get; set; }
        public bool IsCorrect { get; set; }
        public Question Question { get; set; }
        public List<TakeAnswer> TakeAnswers { get; set; }
    }
}
