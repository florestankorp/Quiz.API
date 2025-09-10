namespace Quiz.API.Data;

using Microsoft.EntityFrameworkCore;
using Quiz.API.Models;

public class QuizDb(DbContextOptions<QuizDb> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Stats>().HasData(new Stats { Id = 1, TotalQuizzesTaken = 0, AverageScore = 0 });
    }
}