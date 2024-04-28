using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DermDiag.Migrations
{
    /// <inheritdoc />
    public partial class treatment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End_Date",
                table: "Medicine_Advice");

            migrationBuilder.DropColumn(
                name: "Start_Date",
                table: "Medicine_Advice");

            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "End_Date",
                table: "Medicine_Advice",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Start_Date",
                table: "Medicine_Advice",
                type: "date",
                nullable: true);

            
        }
    }
}
