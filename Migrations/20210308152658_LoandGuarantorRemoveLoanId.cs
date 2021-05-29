using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class LoandGuarantorRemoveLoanId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanGuarantors_LoanApplications_LoanApplicationId",
                table: "LoanGuarantors");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanGuarantors_Loans_LoanId",
                table: "LoanGuarantors");

            migrationBuilder.DropIndex(
                name: "IX_LoanGuarantors_LoanId",
                table: "LoanGuarantors");

            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "LoanGuarantors");

            migrationBuilder.AlterColumn<int>(
                name: "LoanApplicationId",
                table: "LoanGuarantors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanGuarantors_LoanApplications_LoanApplicationId",
                table: "LoanGuarantors",
                column: "LoanApplicationId",
                principalTable: "LoanApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanGuarantors_LoanApplications_LoanApplicationId",
                table: "LoanGuarantors");

            migrationBuilder.AlterColumn<int>(
                name: "LoanApplicationId",
                table: "LoanGuarantors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LoanId",
                table: "LoanGuarantors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LoanGuarantors_LoanId",
                table: "LoanGuarantors",
                column: "LoanId");

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
    }
}
