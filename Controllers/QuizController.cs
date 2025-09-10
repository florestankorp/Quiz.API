using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quiz.API.Data;
using Quiz.API.Models;

namespace Quiz.API.Controllers;

[ApiController]
[Route("[controller]")]
public class QuizController(QuizDb db) : ControllerBase
{
    private readonly QuizDb _db = db;

    [HttpGet("/average-score")]
    public async Task<ActionResult<double?>> Get()
    {
        var stats = await _db.Stats.FirstOrDefaultAsync();
        if (stats == null)
        {
            return NotFound();
        }

        return Ok(stats.AverageScore);
    }

    [HttpPost("/score")]
    public async Task<NoContentResult> Post([FromBody] ScoreDTO scoreDTO)
    {
        var stats = await _db.Stats.FirstOrDefaultAsync();
        if (stats == null)
        {
            stats = new Stats { AverageScore = 0, TotalQuizzesTaken = 0 };
            _db.Stats.Add(stats);
        }

        stats.TotalQuizzesTaken++;
        stats.AverageScore = ((stats.AverageScore * (stats.TotalQuizzesTaken - 1)) + scoreDTO.Score) / stats.TotalQuizzesTaken;

        // Save changes to database
        await _db.SaveChangesAsync();

        return NoContent();

    }
}
