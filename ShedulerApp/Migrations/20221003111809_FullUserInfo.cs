using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShedulerApp.Migrations
{
    public partial class FullUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastExecution",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaskParameter",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TaskType",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastExecution",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaskParameter",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaskType",
                table: "Users");
        }
    }
}
