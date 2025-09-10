using Microsoft.AspNetCore.Mvc;
using Quiz.API.Models;

namespace Quiz.API.Controllers;

[ApiController]
[Route("[controller]")]
public class QuizController : ControllerBase
{


    [HttpGet("/average-score")]
    public async Task<OkResult> Get()
    {
        return await Task.FromResult(Ok());

    }

    [HttpPost("/score")]
    public async Task<NoContentResult> Post([FromBody] ScoreDTO scoreDTO)
    {
        Console.WriteLine(scoreDTO.Score);
        return await Task.FromResult(NoContent());

    }
}
