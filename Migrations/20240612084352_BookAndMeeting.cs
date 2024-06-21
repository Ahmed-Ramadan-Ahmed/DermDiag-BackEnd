using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DermDiag.Migrations
{
    /// <inheritdoc />
    public partial class BookAndMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Book__Doctor_ID__4D94879B",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK__Book__Patient_ID__4CA06362",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK__Book__Payment_ID__4E88ABD4",
                table: "Book");

            migrationBuilder.DropIndex(
                name: "IX_Book_Patient_ID",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "Payment_ID",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Patient_ID",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_ID",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book",
                table: "Book",
                columns: new[] { "Patient_ID", "Doctor_ID", "Payment_ID" });

            migrationBuilder.AddForeignKey(
                name: "FK__Book__Doctor_ID__4D94879B",
                table: "Book",
                column: "Doctor_ID",
                principalTable: "Doctor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Book__Patient_ID__4CA06362",
                table: "Book",
                column: "Patient_ID",
                principalTable: "Patient",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Book__Payment_ID__4E88ABD4",
                table: "Book",
                column: "Payment_ID",
                principalTable: "Payment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Book__Doctor_ID__4D94879B",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK__Book__Patient_ID__4CA06362",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK__Book__Payment_ID__4E88ABD4",
                table: "Book");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "Payment_ID",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_ID",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Patient_ID",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Patient_ID",
                table: "Book",
                column: "Patient_ID");

            migrationBuilder.AddForeignKey(
                name: "FK__Book__Doctor_ID__4D94879B",
                table: "Book",
                column: "Doctor_ID",
                principalTable: "Doctor",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK__Book__Patient_ID__4CA06362",
                table: "Book",
                column: "Patient_ID",
                principalTable: "Patient",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK__Book__Payment_ID__4E88ABD4",
                table: "Book",
                column: "Payment_ID",
                principalTable: "Payment",
                principalColumn: "ID");
        }
    }
}
