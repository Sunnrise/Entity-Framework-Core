using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EF_Core_Configuration.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "As");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bs",
                table: "Bs");

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "Bs",
                newName: "Entity");

            migrationBuilder.AlterColumn<string>(
                name: "Z",
                table: "Entity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DiscriminatorNew",
                table: "Entity",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Y",
                table: "Entity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entity",
                table: "Entity",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Entity",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "DiscriminatorNew",
                table: "Entity");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "Entity");

            migrationBuilder.RenameTable(
                name: "Entity",
                newName: "Bs");

            migrationBuilder.AlterColumn<string>(
                name: "Z",
                table: "Bs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bs",
                table: "Bs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "As",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Y = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_As", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "ConcurrencyCheck", "CreatedDate", "DepartmentId", "Name", "Salary", "Surname" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2024, 6, 12, 17, 17, 46, 234, DateTimeKind.Local).AddTicks(7907), 1, "Alperen", 1000m, "Güneş" },
                    { 2, 0, new DateTime(2024, 6, 12, 17, 17, 46, 234, DateTimeKind.Local).AddTicks(7935), 1, "Alperen", 1000m, "Güneş" }
                });
        }
    }
}
