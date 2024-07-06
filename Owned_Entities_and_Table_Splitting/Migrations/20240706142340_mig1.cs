using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Owned_Entities_and_Table_Splitting.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeName_MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeName_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress_StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress_Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
