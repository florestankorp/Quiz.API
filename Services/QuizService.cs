namespace Quiz.API.Services;

using Microsoft.EntityFrameworkCore;
using Quiz.API.Data;
using Quiz.API.Models;

public interface IQuizService
{
    Task<double?> GetAverageScoreAsync(CancellationToken cancellationToken = default);
    Task AddScoreAsync(int score, CancellationToken cancellationToken = default);
}

public class QuizService(QuizDb db) : IQuizService
{
    private readonly QuizDb _db = db;

    public async Task<double?> GetAverageScoreAsync(CancellationToken cancellationToken = default)
    {
        var stats = await _db.Stats.FirstOrDefaultAsync(cancellationToken);
        return stats?.AverageScore;
    }

    public async Task AddScoreAsync(int score, CancellationToken cancellationToken = default)
    {
        var stats = await _db.Stats.FirstOrDefaultAsync(cancellationToken);
        if (stats == null)
        {
            stats = new Stats { AverageScore = 0, TotalQuizzesTaken = 0 };
            await _db.Stats.AddAsync(stats, cancellationToken);
        }

        // Update running average in one step
        stats.AverageScore = ((stats.AverageScore * stats.TotalQuizzesTaken) + score) / (stats.TotalQuizzesTaken + 1);
        stats.TotalQuizzesTaken++;

        await _db.SaveChangesAsync(cancellationToken);
    }
}
