using Microsoft.AspNetCore.Mvc;
using Quiz.API.Models;
using Quiz.API.Services;

namespace Quiz.API.Controllers;

[ApiController]
[Route("[controller]")]
public class QuizController(IQuizService quizService) : ControllerBase
{
    private readonly IQuizService _quizService = quizService;

    [HttpGet("/average-score")]
    public async Task<ActionResult<double?>> Get(CancellationToken cancellationToken)
    {
        var avg = await _quizService.GetAverageScoreAsync(cancellationToken);

        if (avg is null)
        {
            return NotFound();
        }

        return Ok(avg);
    }

    [HttpPost("/score")]
    public async Task<NoContentResult> Post([FromBody] ScoreDTO scoreDTO, CancellationToken cancellationToken)
    {
        await _quizService.AddScoreAsync(scoreDTO.Score, cancellationToken);
        return NoContent();

    }
}
