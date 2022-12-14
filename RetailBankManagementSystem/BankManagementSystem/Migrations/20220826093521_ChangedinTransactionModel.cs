using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankManagementSystem.Migrations
{
    public partial class ChangedinTransactionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Transactions",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Transactions",
                newName: "UserId");
        }
    }
}
