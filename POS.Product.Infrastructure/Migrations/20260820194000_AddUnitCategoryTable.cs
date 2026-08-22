using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Product.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Units_TenantId_Name",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "Units",
                type: "int",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Units",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemDefined",
                table: "Units",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                table: "ProductVariants",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "ProductVariants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Units_TenantId_Name",
                table: "Units",
                columns: new[] { "TenantId", "Name" },
                unique: true,
                filter: "[TenantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_UnitId",
                table: "ProductVariants",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Units_UnitId",
                table: "ProductVariants",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Units_UnitId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_Units_TenantId_Name",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_UnitId",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "IsSystemDefined",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductVariants");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "ProductVariants");

            migrationBuilder.AlterColumn<Guid>(
                name: "TenantId",
                table: "Units",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_TenantId_Name",
                table: "Units",
                columns: new[] { "TenantId", "Name" },
                unique: true);
        }
    }
}
