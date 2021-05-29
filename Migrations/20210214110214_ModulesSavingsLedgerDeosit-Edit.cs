using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class ModulesSavingsLedgerDeositEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrancastionTypeId",
                table: "SavingDepositLedgers",
                newName: "TransactionTypeId");

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "TransactionTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavingDepositTransactions_TransactionTypeId",
                table: "SavingDepositTransactions",
                column: "TransactionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SavingDepositLedgers_TransactionTypeId",
                table: "SavingDepositLedgers",
                column: "TransactionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavingDepositLedgers_TransactionTypes_TransactionTypeId",
                table: "SavingDepositLedgers",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavingDepositTransactions_TransactionTypes_TransactionTypeId",
                table: "SavingDepositTransactions",
                column: "TransactionTypeId",
                principalTable: "TransactionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavingDepositLedgers_TransactionTypes_TransactionTypeId",
                table: "SavingDepositLedgers");

            migrationBuilder.DropForeignKey(
                name: "FK_SavingDepositTransactions_TransactionTypes_TransactionTypeId",
                table: "SavingDepositTransactions");

            migrationBuilder.DropIndex(
                name: "IX_SavingDepositTransactions_TransactionTypeId",
                table: "SavingDepositTransactions");

            migrationBuilder.DropIndex(
                name: "IX_SavingDepositLedgers_TransactionTypeId",
                table: "SavingDepositLedgers");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "TransactionTypes");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "Modules");

            migrationBuilder.RenameColumn(
                name: "TransactionTypeId",
                table: "SavingDepositLedgers",
                newName: "TrancastionTypeId");
        }
    }
}
