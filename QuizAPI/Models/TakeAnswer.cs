namespace QuizAPI.Models
{
    public class TakeAnswer
    {
        public Guid Id { get; set; }
        public TakeQuestion TakeQuestion { get; set; }
        public Option Option { get; set; }
    }
}
