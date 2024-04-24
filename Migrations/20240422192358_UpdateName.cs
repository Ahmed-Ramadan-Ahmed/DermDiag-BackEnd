//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace DermDiag.Migrations
//{
//    /// <inheritdoc />
//    public partial class UpdateName : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            //migrationBuilder.DropColumn(
//            //    name: "First_Name",
//            //    table: "Patient");

//            migrationBuilder.DropColumn(
//                name: "First_Name",
//                table: "Doctor");

//            //migrationBuilder.RenameColumn(
//            //    name: "Last_Name",
//            //    table: "Patient",
//            //    newName: "Name");

//            migrationBuilder.RenameColumn(
//                name: "Last_Name",
//                table: "Doctor",
//                newName: "Name");

//            migrationBuilder.AlterColumn<DateTime>(
//                name: "DOB",
//                table: "Patient",
//                type: "datetime2",
//                nullable: true,
//                oldClrType: typeof(DateOnly),
//                oldType: "date",
//                oldNullable: true);
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            //migrationBuilder.RenameColumn(
//            //    name: "Name",
//            //    table: "Patient",
//            //    newName: "Last_Name");

//            migrationBuilder.RenameColumn(
//                name: "Name",
//                table: "Doctor",
//                newName: "Last_Name");

//            migrationBuilder.AlterColumn<DateOnly>(
//                name: "DOB",
//                table: "Patient",
//                type: "date",
//                nullable: true,
//                oldClrType: typeof(DateTime),
//                oldType: "datetime2",
//                oldNullable: true);

//            //migrationBuilder.AddColumn<string>(
//            //    name: "First_Name",
//            //    table: "Patient",
//            //    type: "nvarchar(255)",
//            //    maxLength: 255,
//            //    nullable: true);

//            migrationBuilder.AddColumn<string>(
//                name: "First_Name",
//                table: "Doctor",
//                type: "nvarchar(255)",
//                maxLength: 255,
//                nullable: true);
//        }
//    }
//}
