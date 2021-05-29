using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class WithdrawalBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CollectionBankId",
                table: "WithdrawalApplications",
                newName: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalApplications_BankId",
                table: "WithdrawalApplications",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_WithdrawalApplications_Banks_BankId",
                table: "WithdrawalApplications",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WithdrawalApplications_Banks_BankId",
                table: "WithdrawalApplications");

            migrationBuilder.DropIndex(
                name: "IX_WithdrawalApplications_BankId",
                table: "WithdrawalApplications");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "WithdrawalApplications",
                newName: "CollectionBankId");
        }
    }
}
