using Microsoft.EntityFrameworkCore.Migrations;

namespace HrManagerMVC.Migrations
{
    public partial class AddDepartmentHeadIntoEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DepartmentHead",
                table: "Departments",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDepartmentHead",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartmentHead",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "IsDepartmentHead",
                table: "AspNetUsers");
        }
    }
}
