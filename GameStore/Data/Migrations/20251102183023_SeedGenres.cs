using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Puzzle" },
                    { 2, "Platforming" },
                    { 3, "Stealth Action" },
                    { 4, "RPG" },
                    { 5, "Idle" },
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Genres", keyColumn: "Id", keyValue: 1);

            migrationBuilder.DeleteData(table: "Genres", keyColumn: "Id", keyValue: 2);

            migrationBuilder.DeleteData(table: "Genres", keyColumn: "Id", keyValue: 3);

            migrationBuilder.DeleteData(table: "Genres", keyColumn: "Id", keyValue: 4);

            migrationBuilder.DeleteData(table: "Genres", keyColumn: "Id", keyValue: 5);
        }
    }
}
