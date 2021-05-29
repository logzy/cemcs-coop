using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class concurrentloancount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConcurrentLoanCount",
                table: "LoanConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrentLoanCount",
                table: "LoanConfigs");
        }
    }
}
