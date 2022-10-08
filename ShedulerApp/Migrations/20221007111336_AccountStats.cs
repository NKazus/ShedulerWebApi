using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShedulerApp.Migrations
{
    public partial class AccountStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountStats",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false),
                    TasksAdded = table.Column<int>(type: "INTEGER", nullable: false),
                    TasksExecuted = table.Column<int>(type: "INTEGER", nullable: false),
                    TasksDeleted = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStats", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_AccountStats_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountStats");
        }
    }
}
