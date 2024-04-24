//using DermDiag.Models;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace DermDiag.Migrations
//{
//    /// <inheritdoc />
//    public partial class setdoctorid : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropPrimaryKey("PK__Doctor__3214EC2700FA8419", "Doctor");
//            migrationBuilder.DropColumn("ID", "Doctor");
//            migrationBuilder.AddColumn<int>("ID", "Doctor").Annotation("SqlServer:Identity", "1, 1");
//            migrationBuilder.AddPrimaryKey("DoctorPK", "Doctor", "ID");

//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropIndex("DoctorPK", "Doctor");
//            migrationBuilder.DropColumn("ID", "Doctor");
//            migrationBuilder.AddColumn<int>("ID", "Doctor");
//            migrationBuilder.AddPrimaryKey("DoctorPK", "Doctor", "ID");

//        }
//    }
//}
