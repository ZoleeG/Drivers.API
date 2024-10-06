using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DriverInfo.API.Migrations
{
    /// <inheritdoc />
    public partial class DriverInfoDBInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    GridPosition = table.Column<int>(type: "INTEGER", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    DriverId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wins_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Wins_DriverId",
                table: "Wins",
                column: "DriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wins");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
