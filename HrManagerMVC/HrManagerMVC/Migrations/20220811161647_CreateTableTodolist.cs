using Microsoft.EntityFrameworkCore.Migrations;

namespace HrManagerMVC.Migrations
{
    public partial class CreateTableTodolist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todolists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true),
                    EmloyeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todolists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Todolists_AspNetUsers_EmloyeeId",
                        column: x => x.EmloyeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todolists_EmloyeeId",
                table: "Todolists",
                column: "EmloyeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todolists");
        }
    }
}
