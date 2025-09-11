namespace Quiz.API.Models;

using Quiz.API.Configuration;

public class Stats
{
    public int Id { get; set; } = 1;
    public int TotalQuizzesTaken { get; set; } = Defaults.Quiz.InitialCount;
    public double AverageScore { get; set; } = Defaults.Quiz.InitialAverageScore;
}
