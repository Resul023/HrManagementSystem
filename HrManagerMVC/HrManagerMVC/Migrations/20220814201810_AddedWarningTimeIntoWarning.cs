using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrManagerMVC.Migrations
{
    public partial class AddedWarningTimeIntoWarning : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warnings_AspNetUsers_EmployeeId1",
                table: "Warnings");

            migrationBuilder.DropIndex(
                name: "IX_Warnings_EmployeeId1",
                table: "Warnings");

            migrationBuilder.DropColumn(
                name: "EmployeeId1",
                table: "Warnings");

            migrationBuilder.AlterColumn<string>(
                name: "WarningMessage",
                table: "Warnings",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Warnings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "WarningTime",
                table: "Warnings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Warnings_EmployeeId",
                table: "Warnings",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warnings_AspNetUsers_EmployeeId",
                table: "Warnings",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warnings_AspNetUsers_EmployeeId",
                table: "Warnings");

            migrationBuilder.DropIndex(
                name: "IX_Warnings_EmployeeId",
                table: "Warnings");

            migrationBuilder.DropColumn(
                name: "WarningTime",
                table: "Warnings");

            migrationBuilder.AlterColumn<string>(
                name: "WarningMessage",
                table: "Warnings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Warnings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId1",
                table: "Warnings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warnings_EmployeeId1",
                table: "Warnings",
                column: "EmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Warnings_AspNetUsers_EmployeeId1",
                table: "Warnings",
                column: "EmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
