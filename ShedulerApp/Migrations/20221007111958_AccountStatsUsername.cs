using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShedulerApp.Migrations
{
    public partial class AccountStatsUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AccountStats",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AccountStats");
        }
    }
}
