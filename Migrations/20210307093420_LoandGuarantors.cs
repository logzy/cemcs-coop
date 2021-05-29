using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace COOP.Banking.Migrations
{
    public partial class LoandGuarantors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GuarantorMaximumAmount",
                table: "LoanConfigs",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ServiceYearDuration",
                table: "LoanConfigs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "UseYearsOfService",
                table: "LoanConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "GuarantorApprovalCount",
                table: "LoanApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LoanGuarantor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanId = table.Column<int>(type: "int", nullable: false),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GurantorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GurantorEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoanApplicationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanGuarantor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanGuarantor_LoanApplications_LoanApplicationId",
                        column: x => x.LoanApplicationId,
                        principalTable: "LoanApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanGuarantor_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanGuarantor_LoanApplicationId",
                table: "LoanGuarantor",
                column: "LoanApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanGuarantor_LoanId",
                table: "LoanGuarantor",
                column: "LoanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanGuarantor");

            migrationBuilder.DropColumn(
                name: "GuarantorMaximumAmount",
                table: "LoanConfigs");

            migrationBuilder.DropColumn(
                name: "ServiceYearDuration",
                table: "LoanConfigs");

            migrationBuilder.DropColumn(
                name: "UseYearsOfService",
                table: "LoanConfigs");

            migrationBuilder.DropColumn(
                name: "GuarantorApprovalCount",
                table: "LoanApplications");
        }
    }
}
