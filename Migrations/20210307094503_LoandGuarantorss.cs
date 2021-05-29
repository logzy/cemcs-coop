using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class LoandGuarantorss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanGuarantor_LoanApplications_LoanApplicationId",
                table: "LoanGuarantor");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanGuarantor_Loans_LoanId",
                table: "LoanGuarantor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanGuarantor",
                table: "LoanGuarantor");

            migrationBuilder.RenameTable(
                name: "LoanGuarantor",
                newName: "LoanGuarantors");

            migrationBuilder.RenameIndex(
                name: "IX_LoanGuarantor_LoanId",
                table: "LoanGuarantors",
                newName: "IX_LoanGuarantors_LoanId");

            migrationBuilder.RenameIndex(
                name: "IX_LoanGuarantor_LoanApplicationId",
                table: "LoanGuarantors",
                newName: "IX_LoanGuarantors_LoanApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanGuarantors",
                table: "LoanGuarantors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanGuarantors_LoanApplications_LoanApplicationId",
                table: "LoanGuarantors",
                column: "LoanApplicationId",
                principalTable: "LoanApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanGuarantors_Loans_LoanId",
                table: "LoanGuarantors",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanGuarantors_LoanApplications_LoanApplicationId",
                table: "LoanGuarantors");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanGuarantors_Loans_LoanId",
                table: "LoanGuarantors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanGuarantors",
                table: "LoanGuarantors");

            migrationBuilder.RenameTable(
                name: "LoanGuarantors",
                newName: "LoanGuarantor");

            migrationBuilder.RenameIndex(
                name: "IX_LoanGuarantors_LoanId",
                table: "LoanGuarantor",
                newName: "IX_LoanGuarantor_LoanId");

            migrationBuilder.RenameIndex(
                name: "IX_LoanGuarantors_LoanApplicationId",
                table: "LoanGuarantor",
                newName: "IX_LoanGuarantor_LoanApplicationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanGuarantor",
                table: "LoanGuarantor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanGuarantor_LoanApplications_LoanApplicationId",
                table: "LoanGuarantor",
                column: "LoanApplicationId",
                principalTable: "LoanApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanGuarantor_Loans_LoanId",
                table: "LoanGuarantor",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
