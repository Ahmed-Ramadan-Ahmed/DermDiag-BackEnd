using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DermDiag.Migrations
{
    /// <inheritdoc />
    public partial class PaymentModelUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "Payment");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Payment",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            // Drop foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "FK__Book__Payment_ID__4E88ABD4",
                table: "Book");

            // Drop primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK__Payment__3214EC27357C9B09",
                table: "Payment");

            // Drop the old ID column
            migrationBuilder.DropColumn(
                name: "ID",
                table: "Payment");

            // Add the new ID column with IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "NewID",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Rename the new column to the old column's name
            migrationBuilder.RenameColumn(
                name: "NewID",
                table: "Payment",
                newName: "ID");

            // Add the primary key constraint back
            migrationBuilder.AddPrimaryKey(
                name: "PK__Payment__3214EC27357C9B09",
                table: "Payment",
                column: "ID");

            // Recreate foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK__Book__Payment_ID__4E88ABD4",
                table: "Book",
                column: "Payment_ID",
                principalTable: "Payment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            // Add other columns
            migrationBuilder.AddColumn<int>(
                name: "ReceiverID",
                table: "Payment",
                type: "int",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SenderID",
                table: "Payment",
                type: "int",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverID",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "SenderID",
                table: "Payment");

            // Drop foreign key constraints
            migrationBuilder.DropForeignKey(
                name: "FK__Book__Payment_ID__4E88ABD4",
                table: "Book");

            // Drop primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK__Payment__3214EC27357C9B09",
                table: "Payment");

            // Drop the new ID column
            migrationBuilder.DropColumn(
                name: "ID",
                table: "Payment");

            // Add the old ID column back without IDENTITY property
            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Payment",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Payment",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "Payment",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            // Add the primary key constraint back
            migrationBuilder.AddPrimaryKey(
                name: "PK__Payment__3214EC27357C9B09",
                table: "Payment",
                column: "ID");

            // Recreate foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK__Book__Payment_ID__4E88ABD4",
                table: "Book",
                column: "Payment_ID",
                principalTable: "Payment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
