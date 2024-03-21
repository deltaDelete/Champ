using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Champ.API._1
{
    /// <inheritdoc />
    public partial class PharmacyFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ExpirationDate",
                table: "DrugStocks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ReceiptDate",
                table: "DrugStocks",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Drugs",
                type: "varchar(4096)",
                maxLength: 4096,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DrugWarehouse",
                columns: table => new
                {
                    DrugsDrugId = table.Column<long>(type: "bigint", nullable: false),
                    WarehousesWarehouseId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugWarehouse", x => new { x.DrugsDrugId, x.WarehousesWarehouseId });
                    table.ForeignKey(
                        name: "FK_DrugWarehouse_Drugs_DrugsDrugId",
                        column: x => x.DrugsDrugId,
                        principalTable: "Drugs",
                        principalColumn: "DrugId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrugWarehouse_Warehouses_WarehousesWarehouseId",
                        column: x => x.WarehousesWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DrugWarehouse_WarehousesWarehouseId",
                table: "DrugWarehouse",
                column: "WarehousesWarehouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugWarehouse");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "DrugStocks");

            migrationBuilder.DropColumn(
                name: "ReceiptDate",
                table: "DrugStocks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Drugs");
        }
    }
}
