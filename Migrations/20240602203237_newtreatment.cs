using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DermDiag.Migrations
{
    /// <inheritdoc />
    public partial class newtreatment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicine_Advice",
                table: "Medicine_Advice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicine_Advice",
                table: "Medicine_Advice",
                columns: new[] { "Doctor_ID", "Patient_ID", "Medicine_Name" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicine_Advice",
                table: "Medicine_Advice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicine_Advice",
                table: "Medicine_Advice",
                columns: new[] { "Doctor_ID", "Patient_ID" });
        }
    }
}
