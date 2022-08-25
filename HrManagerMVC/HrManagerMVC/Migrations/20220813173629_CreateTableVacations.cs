using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HrManagerMVC.Migrations
{
    public partial class CreateTableVacations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vacations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StarDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    VacationAnswer = table.Column<string>(nullable: true),
                    Text = table.Column<string>(maxLength: 250, nullable: false),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vacations_AspNetUsers_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_EmployeeId",
                table: "Vacations",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vacations");
        }
    }
}
