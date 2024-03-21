﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Champ.API.Migrations
{
    /// <inheritdoc />
    public partial class MeasurePriceAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Measures",
                type: "decimal(10, 2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Measures");
        }
    }
}
