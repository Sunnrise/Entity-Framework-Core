using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Core_Configuration.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Departments_AlperenId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_AlperenId",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "AlperenId",
                table: "Persons",
                newName: "NotMappedProperty");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Persons",
                newName: "DepartmentId1");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId1",
                table: "Persons",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PrimaryKeyProperty",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentId",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "PrimaryKeyProperty");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Departments_DepartmentId1",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_DepartmentId1",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PrimaryKeyProperty",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "NotMappedProperty",
                table: "Persons",
                newName: "AlperenId");

            migrationBuilder.RenameColumn(
                name: "DepartmentId1",
                table: "Persons",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Persons",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_AlperenId",
                table: "Persons",
                column: "AlperenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Departments_AlperenId",
                table: "Persons",
                column: "AlperenId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
