using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class RepaymentDateCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "LoanRepayments");

            migrationBuilder.RenameColumn(
                name: "RepaymentDte",
                table: "LoanRepayments",
                newName: "RepaymentDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RepaymentDate",
                table: "LoanRepayments",
                newName: "RepaymentDte");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "LoanRepayments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
