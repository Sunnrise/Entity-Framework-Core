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
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Persons",
                newName: "FullName2");

            migrationBuilder.AlterColumn<string>(
                name: "FullName2",
                table: "Persons",
                type: "Text2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Text")
                .Annotation("Relational:ColumnOrder", 7);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName2",
                table: "Persons",
                newName: "FullName");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Persons",
                type: "Text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Text2")
                .OldAnnotation("Relational:ColumnOrder", 7);
        }
    }
}
