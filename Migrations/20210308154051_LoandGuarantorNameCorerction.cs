using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class LoandGuarantorNameCorerction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GurantorName",
                table: "LoanGuarantors",
                newName: "GuarantorName");

            migrationBuilder.RenameColumn(
                name: "GurantorEmail",
                table: "LoanGuarantors",
                newName: "GuarantorEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuarantorName",
                table: "LoanGuarantors",
                newName: "GurantorName");

            migrationBuilder.RenameColumn(
                name: "GuarantorEmail",
                table: "LoanGuarantors",
                newName: "GurantorEmail");
        }
    }
}
