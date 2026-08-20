using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantIdToApplicationRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Permissions_Name",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AspNetRoles");
        }
    }
}
