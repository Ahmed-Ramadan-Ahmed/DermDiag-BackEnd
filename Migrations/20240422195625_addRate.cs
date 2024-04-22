using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DermDiag.Migrations
{
    /// <inheritdoc />
    public partial class addRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Doctor",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Doctor");
        }
    }
}
