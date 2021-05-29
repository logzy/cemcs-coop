using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class DepositTransactionUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "SavingDepositTransactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "SavingDepositTransactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasReflected",
                table: "SavingDepositTransactions",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "SavingDepositTransactions");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "SavingDepositTransactions");

            migrationBuilder.DropColumn(
                name: "HasReflected",
                table: "SavingDepositTransactions");
        }
    }
}
