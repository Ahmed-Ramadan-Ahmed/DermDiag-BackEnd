using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DermDiag.Migrations
{
    /// <inheritdoc />
    public partial class update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Medicine___Docto__59063A47",
                table: "Medicine_Advice");

            migrationBuilder.DropForeignKey(
                name: "FK__Medicine___Patie__5812160E",
                table: "Medicine_Advice");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_Advice_Doctor_ID",
                table: "Medicine_Advice");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Medicine_Advice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Patient_ID",
                table: "Medicine_Advice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Medicine_Name",
                table: "Medicine_Advice",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Frequency",
                table: "Medicine_Advice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_ID",
                table: "Medicine_Advice",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Medicine_Advice",
                table: "Medicine_Advice",
                columns: new[] { "Doctor_ID", "Patient_ID" });

            migrationBuilder.AddForeignKey(
                name: "FK__Medicine___Docto__59063A47",
                table: "Medicine_Advice",
                column: "Doctor_ID",
                principalTable: "Doctor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Medicine___Patie__5812160E",
                table: "Medicine_Advice",
                column: "Patient_ID",
                principalTable: "Patient",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Medicine___Docto__59063A47",
                table: "Medicine_Advice");

            migrationBuilder.DropForeignKey(
                name: "FK__Medicine___Patie__5812160E",
                table: "Medicine_Advice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Medicine_Advice",
                table: "Medicine_Advice");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Medicine_Advice",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Medicine_Name",
                table: "Medicine_Advice",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "Frequency",
                table: "Medicine_Advice",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Patient_ID",
                table: "Medicine_Advice",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Doctor_ID",
                table: "Medicine_Advice",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_Advice_Doctor_ID",
                table: "Medicine_Advice",
                column: "Doctor_ID");

            migrationBuilder.AddForeignKey(
                name: "FK__Medicine___Docto__59063A47",
                table: "Medicine_Advice",
                column: "Doctor_ID",
                principalTable: "Doctor",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK__Medicine___Patie__5812160E",
                table: "Medicine_Advice",
                column: "Patient_ID",
                principalTable: "Patient",
                principalColumn: "ID");
        }
    }
}
