using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aspekt.Loans.Data.Database.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "PaidOffAt",
                schema: "Loans",
                table: "Loans",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidOffAt",
                schema: "Loans",
                table: "Loans");
        }
    }
}
