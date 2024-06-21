using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DermDiag.Migrations
{
    /// <inheritdoc />
    public partial class EmailMessageAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Consulte__2FF13E69B0C57837",
                table: "Consulte");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Consulte__2FF13E69B0C57837",
                table: "Consulte",
                columns: new[] { "Patient_ID", "Doctor_ID", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK__Consulte__2FF13E69B0C57837",
                table: "Consulte");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Consulte__2FF13E69B0C57837",
                table: "Consulte",
                columns: new[] { "Patient_ID", "Doctor_ID" });
        }
    }
}
