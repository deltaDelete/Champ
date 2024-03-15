using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Champ.API.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentToHospitalization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "Hospitalizations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Hospitalizations_DepartmentId",
                table: "Hospitalizations",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitalizations_Departments_DepartmentId",
                table: "Hospitalizations",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hospitalizations_Departments_DepartmentId",
                table: "Hospitalizations");

            migrationBuilder.DropIndex(
                name: "IX_Hospitalizations_DepartmentId",
                table: "Hospitalizations");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Hospitalizations");
        }
    }
}
