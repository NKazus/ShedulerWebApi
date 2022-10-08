using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShedulerApp.Migrations
{
    public partial class AccountStatsSeparated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountStats_Accounts_AccountId",
                table: "AccountStats");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "AccountStats",
                newName: "AccountStatsId");

            migrationBuilder.AlterColumn<int>(
                name: "AccountStatsId",
                table: "AccountStats",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountStatsId",
                table: "AccountStats",
                newName: "AccountId");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "AccountStats",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountStats_Accounts_AccountId",
                table: "AccountStats",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
