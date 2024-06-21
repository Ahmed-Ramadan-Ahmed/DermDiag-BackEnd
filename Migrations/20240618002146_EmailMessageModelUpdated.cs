using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DermDiag.Migrations
{
    /// <inheritdoc />
    public partial class EmailMessageModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailMessage",
                table: "EmailMessage");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmailMessage");

            migrationBuilder.DropColumn(
                name: "From",
                table: "EmailMessage");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "SmtpSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "SmtpSettings");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EmailMessage",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "EmailMessage",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailMessage",
                table: "EmailMessage",
                column: "Id");
        }
    }
}
