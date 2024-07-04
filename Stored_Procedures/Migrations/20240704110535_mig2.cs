using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stored_Procedures.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                CREATE PROCEDURE sp_GetPersonOrders
                AS
                    SELECT p.Name, COUNT(*) [Count] FROM Persons p
                    JOIN Orders o 
                        ON p.PersonId = o.PersonId
                    GROUP BY p.Name
                    ORDER BY COUNT(*) DESC
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE sp_GetPersonOrders");
        }
    }
}
