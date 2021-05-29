using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class LoanInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Intrest",
                table: "LoanRepayments");

            migrationBuilder.AddColumn<decimal>(
                name: "Interest",
                table: "LoanRepayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "LoanCondition",
                table: "LoanApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interest",
                table: "LoanRepayments");

            migrationBuilder.DropColumn(
                name: "LoanCondition",
                table: "LoanApplications");

            migrationBuilder.AddColumn<float>(
                name: "Intrest",
                table: "LoanRepayments",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
