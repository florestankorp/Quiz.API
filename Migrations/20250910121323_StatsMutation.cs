using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quiz.API.Migrations
{
    /// <inheritdoc />
    public partial class StatsMutation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stats",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Stats",
                columns: new[] { "Id", "AverageScore", "TotalQuizzesTaken" },
                values: new object[] { 1, 0.0, 0 });
        }
    }
}
