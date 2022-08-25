using Microsoft.EntityFrameworkCore.Migrations;

namespace HrManagerMVC.Migrations
{
    public partial class AddedDepartmentIdInEmployeesAndDeleteEmployeeIdInDepartments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_EmployeeId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_EmployeeId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EmployeeId",
                table: "Departments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_EmployeeId",
                table: "Departments",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
