using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fulbito_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedfieldrequirementsinTournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "TournamentsPlayerStats",
                newName: "UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TournamentsPlayerStats",
                newName: "PlayerId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
