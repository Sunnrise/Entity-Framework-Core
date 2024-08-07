﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Views.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"

                CREATE VIEW vm_PersonOrders 
                AS
                    SELECT p.Name, COUNT(*) [Count] FROM 
                Persons p
                    INNER JOIN Orders o 
                    ON p.PersonId = o.PersonId
                    GROUP BY p.Name;
                    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vm_PersonOrders;");

        }
    }
}
