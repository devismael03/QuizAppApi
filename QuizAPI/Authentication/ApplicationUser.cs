using Microsoft.AspNetCore.Identity;
using QuizAPI.Models;

namespace QuizAPI.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Quiz> Quizzes { get; set; }
        public List<Take> Takes { get; set; }
    }
}
