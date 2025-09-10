namespace Quiz.API.Data;

using Microsoft.EntityFrameworkCore;
using Quiz.API.Models;

public class QuizDb(DbContextOptions<QuizDb> options) : DbContext(options)
{
    public DbSet<Stats> Stats { get; set; } = null!;
}