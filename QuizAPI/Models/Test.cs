namespace QuizAPI.Models
{
    public class Test
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EasyQuestionCount { get; set; }
        public int MediumQuestionCount { get; set; }
        public int HardQuestionCount { get; set; }
        public Quiz Quiz { get; set; }
        public List<Take> Takes { get; set; }
    }
}
