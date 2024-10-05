using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DriverInfo.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDriverInfoDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "The guy who always wins.", "Max Verstappen" },
                    { 2, "Won F1-championship 7 times.", "Lewis Hamilton" }
                });

            migrationBuilder.InsertData(
                table: "Wins",
                columns: new[] { "Id", "DriverId", "GridPosition", "Name", "Year" },
                values: new object[,]
                {
                    { 1, 1, 4, "Spanish Grand Prix", 2016 },
                    { 2, 1, 3, "Malaysian Grand Prix", 2017 },
                    { 3, 1, 2, "Mexican Grand Prix", 2017 },
                    { 4, 2, 1, "Canadian Grand Prix", 2007 },
                    { 5, 2, 1, "United States Grand Prix", 2007 },
                    { 6, 2, 1, "Hungarian Grand Prix", 2007 },
                    { 7, 2, 1, "Japanese Grand Prix", 2007 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Wins",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Wins",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Wins",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Wins",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Wins",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Wins",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Wins",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
