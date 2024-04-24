using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DermDiag.Migrations
{
    /// <inheritdoc />
    public partial class lastupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Fees = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    No_Reviews = table.Column<int>(type: "int", nullable: true),
                    Rating = table.Column<float>(type: "real", nullable: true),
                    No_Sessions = table.Column<int>(type: "int", nullable: true),
                    Image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Acceptance_Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Doctor__3214EC277293A442", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patient__3214EC277AF761B0", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Receiver = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__3214EC27357C9B09", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Consulte",
                columns: table => new
                {
                    Patient_ID = table.Column<int>(type: "int", nullable: false),
                    Doctor_ID = table.Column<int>(type: "int", nullable: false),
                    Doctor_Attendance = table.Column<bool>(type: "bit", nullable: true),
                    Patient_Attendance = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Consulte__2FF13E69B0C57837", x => new { x.Patient_ID, x.Doctor_ID });
                    table.ForeignKey(
                        name: "FK__Consulte__Doctor__5629CD9C",
                        column: x => x.Doctor_ID,
                        principalTable: "Doctor",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Consulte__Patien__5535A963",
                        column: x => x.Patient_ID,
                        principalTable: "Patient",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    Patient_ID = table.Column<int>(type: "int", nullable: false),
                    Doctor_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorite__2FF13E69A503B9A2", x => new { x.Patient_ID, x.Doctor_ID });
                    table.ForeignKey(
                        name: "FK__Favorite__Doctor__52593CB8",
                        column: x => x.Doctor_ID,
                        principalTable: "Doctor",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Favorite__Patien__5165187F",
                        column: x => x.Patient_ID,
                        principalTable: "Patient",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Medicine_Advice",
                columns: table => new
                {
                    Patient_ID = table.Column<int>(type: "int", nullable: true),
                    Doctor_ID = table.Column<int>(type: "int", nullable: true),
                    Medicine_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Start_Date = table.Column<DateOnly>(type: "date", nullable: true),
                    End_Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Frequency = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__Medicine___Docto__59063A47",
                        column: x => x.Doctor_ID,
                        principalTable: "Doctor",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Medicine___Patie__5812160E",
                        column: x => x.Patient_ID,
                        principalTable: "Patient",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Patient_Model_History",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patient_ID = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: true),
                    Model_Result = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Patient___3214EC275616F051", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Patient_M__Patie__46E78A0C",
                        column: x => x.Patient_ID,
                        principalTable: "Patient",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Patient_ID = table.Column<int>(type: "int", nullable: true),
                    Doctor_ID = table.Column<int>(type: "int", nullable: true),
                    Payment_ID = table.Column<int>(type: "int", nullable: true),
                    Appointment_Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__Book__Doctor_ID__4D94879B",
                        column: x => x.Doctor_ID,
                        principalTable: "Doctor",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Book__Patient_ID__4CA06362",
                        column: x => x.Patient_ID,
                        principalTable: "Patient",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Book__Payment_ID__4E88ABD4",
                        column: x => x.Payment_ID,
                        principalTable: "Payment",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Model_Input_Images",
                columns: table => new
                {
                    Model_History_ID = table.Column<int>(type: "int", nullable: true),
                    Image_URL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__Model_Inp__Model__48CFD27E",
                        column: x => x.Model_History_ID,
                        principalTable: "Patient_Model_History",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_Doctor_ID",
                table: "Book",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Patient_ID",
                table: "Book",
                column: "Patient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Payment_ID",
                table: "Book",
                column: "Payment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Consulte_Doctor_ID",
                table: "Consulte",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__Doctor__A9D105344BCD1DAD",
                table: "Doctor",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Doctor_ID",
                table: "Favorite",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_Advice_Doctor_ID",
                table: "Medicine_Advice",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_Advice_Patient_ID",
                table: "Medicine_Advice",
                column: "Patient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Model_Input_Images_Model_History_ID",
                table: "Model_Input_Images",
                column: "Model_History_ID");

            migrationBuilder.CreateIndex(
                name: "UQ__Patient__A9D105346DCA7C3A",
                table: "Patient",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Model_History_Patient_ID",
                table: "Patient_Model_History",
                column: "Patient_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Consulte");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "Medicine_Advice");

            migrationBuilder.DropTable(
                name: "Model_Input_Images");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient_Model_History");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
