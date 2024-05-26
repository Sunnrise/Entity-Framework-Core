using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace One_to_One_Relationship.Migrations
{
    /// <inheritdoc />
    public partial class mig_DataAnnotations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EmployeeAddresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EmployeeAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
