using Microsoft.EntityFrameworkCore;
using Quiz.API.Configuration;
using Quiz.API.Data;
using Quiz.API.Models;
using Quiz.API.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Quiz") ?? "Data Source=Quiz.db";

// Add services to the container.
builder.Services.AddSqlite<QuizDb>(connectionString);
builder.Services.AddControllers();
builder.Services.AddScoped<IQuizService, QuizService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Apply migrations and create database on startup.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<QuizDb>();

    db.Database.Migrate();

    if (!await db.Stats.AnyAsync())
    {
        db.Stats.Add(new Stats { TotalQuizzesTaken = Defaults.Quiz.InitialCount, AverageScore = Defaults.Quiz.InitialAverageScore });
        await db.SaveChangesAsync();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
