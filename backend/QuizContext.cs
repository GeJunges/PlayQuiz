using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options)
        {
        }

        DbSet<Question> Questions { get; set; }

        DbSet<Quiz> Quizzes { get; set; }
    }
}