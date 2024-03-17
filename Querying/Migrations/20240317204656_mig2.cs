using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Querying.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Components",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_ProductId",
                table: "Components",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Products_ProductId",
                table: "Components",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Products_ProductId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_ProductId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Components");
        }
    }
}
