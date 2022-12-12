namespace QuizAPI.Controllers.DTO
{
    public class GetQuizzesResponse
    {
        public Guid QuizId { get; set; }
        public string QuizTitle { get; set; }
        public List<TestsIndexDto>? Tests { get; set; }

    }
}
