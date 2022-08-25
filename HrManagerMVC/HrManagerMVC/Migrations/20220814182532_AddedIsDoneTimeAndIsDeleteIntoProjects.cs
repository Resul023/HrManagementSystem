using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrManagerMVC.Migrations
{
    public partial class AddedIsDoneTimeAndIsDeleteIntoProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Projects",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "IsDoneTime",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "IsDoneTime",
                table: "Projects");
        }
    }
}
