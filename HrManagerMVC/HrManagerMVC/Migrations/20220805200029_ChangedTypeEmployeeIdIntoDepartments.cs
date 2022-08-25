using Microsoft.EntityFrameworkCore.Migrations;

namespace HrManagerMVC.Migrations
{
    public partial class ChangedTypeEmployeeIdIntoDepartments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_EmployeeId1",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_EmployeeId1",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "Departments");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Departments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_EmployeeId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_EmployeeId",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Departments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId1",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EmployeeId1",
                table: "Departments",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_EmployeeId1",
                table: "Departments",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
