using Microsoft.EntityFrameworkCore.Migrations;

namespace HrManagerMVC.Migrations
{
    public partial class CreateTableWarnings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Warnings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WarningMessage = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warnings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warnings_AspNetUsers_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Warnings_EmployeeId1",
                table: "Warnings",
                column: "EmployeeId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Warnings");
        }
    }
}
