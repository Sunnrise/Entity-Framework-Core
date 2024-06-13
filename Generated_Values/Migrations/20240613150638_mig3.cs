using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generated_Values.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalGain",
                table: "Persons",
                type: "int",
                nullable: false,
                computedColumnSql: "([Premium] + [Salary])*10",
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "([Premium] + []Salary])*10");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalGain",
                table: "Persons",
                type: "int",
                nullable: false,
                computedColumnSql: "([Premium] + []Salary])*10",
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "([Premium] + [Salary])*10");
        }
    }
}
