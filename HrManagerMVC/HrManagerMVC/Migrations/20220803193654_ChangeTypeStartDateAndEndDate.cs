using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrManagerMVC.Migrations
{
    public partial class ChangeTypeStartDateAndEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartDate",
                table: "Events",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "EndDate",
                table: "Events",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
