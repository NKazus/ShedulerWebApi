using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShedulerApp.Migrations
{
    public partial class TaskID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Users",
                newName: "TaskID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaskID",
                table: "Users",
                newName: "UserID");
        }
    }
}
