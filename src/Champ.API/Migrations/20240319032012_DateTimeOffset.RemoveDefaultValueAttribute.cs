using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Champ.API.Migrations
{
    /// <inheritdoc />
    public partial class DateTimeOffsetRemoveDefaultValueAttribute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateOfBirth",
                table: "Patients",
                type: "TIMESTAMP",
                defaultValue: "CURRENT_TIMESTAMP",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "MeasureDate",
                table: "Measures",
                type: "TIMESTAMP",
                defaultValue: "CURRENT_TIMESTAMP",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TIMESTAMP")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateStart",
                table: "Hospitalizations",
                type: "TIMESTAMP",
                defaultValue: "CURRENT_TIMESTAMP",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateEnd",
                table: "Hospitalizations",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateOfBirth",
                table: "Patients",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TIMESTAMP")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "MeasureDate",
                table: "Measures",
                type: "TIMESTAMP",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TIMESTAMP")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateStart",
                table: "Hospitalizations",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TIMESTAMP")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "DateEnd",
                table: "Hospitalizations",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TIMESTAMP")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn);
        }
    }
}
