using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fulbito_api.Migrations
{
    /// <inheritdoc />
    public partial class RenamedPlayerStatsFromTournamentandremovedPositionfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "TournamentsPlayerStats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "TournamentsPlayerStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
