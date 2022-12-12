namespace QuizAPI.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string QuestionText { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public QuizType Type { get; set; }
        public Quiz Quiz { get; set; }
        public List<Option> Options { get; set; }
        public List<TakeQuestion> TakeQuestions { get; set; }

    }
}
