using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EF_Core_Configuration.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Departments_DepartmentId1",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_DepartmentId1",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Persons");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Persons",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Computed",
                table: "Examples",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "[x] + [y]");

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "ConcurrencyCheck", "CreatedDate", "DepartmentId", "Name", "Salary", "Surname" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2024, 6, 12, 16, 35, 32, 609, DateTimeKind.Local).AddTicks(3532), 1, "Alperen", 1000m, "Güneş" },
                    { 2, 0, new DateTime(2024, 6, 12, 16, 35, 32, 609, DateTimeKind.Local).AddTicks(3558), 1, "Alperen", 1000m, "Güneş" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DepartmentId",
                table: "Persons",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Departments_DepartmentId",
                table: "Persons",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Departments_DepartmentId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_DepartmentId",
                table: "Persons");

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "DepartmentId",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId1",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Persons",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<int>(
                name: "Computed",
                table: "Examples",
                type: "int",
                nullable: false,
                computedColumnSql: "[x] + [y]",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DepartmentId1",
                table: "Persons",
                column: "DepartmentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Departments_DepartmentId1",
                table: "Persons",
                column: "DepartmentId1",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
