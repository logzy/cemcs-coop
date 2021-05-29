using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class loanconfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowTenureAdjustment",
                table: "LoanConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AllowTopUp",
                table: "LoanConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GuarantorCount",
                table: "LoanConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresGuarantors",
                table: "LoanConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowTenureAdjustment",
                table: "LoanConfigs");

            migrationBuilder.DropColumn(
                name: "AllowTopUp",
                table: "LoanConfigs");

            migrationBuilder.DropColumn(
                name: "GuarantorCount",
                table: "LoanConfigs");

            migrationBuilder.DropColumn(
                name: "RequiresGuarantors",
                table: "LoanConfigs");
        }
    }
}
