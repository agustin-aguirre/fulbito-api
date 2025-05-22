using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fulbito_api.Migrations
{
    /// <inheritdoc />
    public partial class AddedTotalPointsfieldtoplayerstournamentstats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalPoints",
                table: "TournamentsPlayerStats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPoints",
                table: "TournamentsPlayerStats");
        }
    }
}
