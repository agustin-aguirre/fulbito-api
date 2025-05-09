using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fulbito_api.Migrations
{
    /// <inheritdoc />
    public partial class Initialtablesadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PlayedMatchesCount = table.Column<int>(type: "int", nullable: false),
                    TotalMatches = table.Column<int>(type: "int", nullable: false),
                    PointsPerWonMatch = table.Column<int>(type: "int", nullable: false),
                    PointsPerTiedMatch = table.Column<int>(type: "int", nullable: false),
                    PointsPerLostMatch = table.Column<int>(type: "int", nullable: false),
                    PointsPerAssertedMatch = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModification = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentsPlayerStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    GoalCount = table.Column<int>(type: "int", nullable: false),
                    AssitenciesCount = table.Column<int>(type: "int", nullable: false),
                    PlayedMatchesCount = table.Column<int>(type: "int", nullable: false),
                    WonMatchesCount = table.Column<int>(type: "int", nullable: false),
                    TiedMatchesCount = table.Column<int>(type: "int", nullable: false),
                    LostMatchesCount = table.Column<int>(type: "int", nullable: false),
                    AssertedMatchesCount = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModification = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentsPlayerStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "TournamentsPlayerStats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
