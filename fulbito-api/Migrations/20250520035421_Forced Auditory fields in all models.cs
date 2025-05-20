﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fulbito_api.Migrations
{
    /// <inheritdoc />
    public partial class ForcedAuditoryfieldsinallmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModification",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModification",
                table: "Users");
        }
    }
}
