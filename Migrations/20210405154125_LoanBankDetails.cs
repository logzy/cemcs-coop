using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace COOP.Banking.Migrations
{
    public partial class LoanBankDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "LoanApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "LoanApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "LoanApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransferApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    SourceSavingsType = table.Column<int>(type: "int", nullable: false),
                    DestinationSavingsType = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    MethodOfCollectionId = table.Column<int>(type: "int", nullable: false),
                    ApprovalCount = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferApplications_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaiverApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ApplicatiinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalWaiverFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Commnents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalCount = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModeOfPaymentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaiverApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaiverApplication_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WaiverApplication_ModeOfPayments_ModeOfPaymentId",
                        column: x => x.ModeOfPaymentId,
                        principalTable: "ModeOfPayments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WaiverTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaiverTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WithdrawalApplications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    SourceSavingsType = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MethodOfCollectionId = table.Column<int>(type: "int", nullable: false),
                    ApprovalCount = table.Column<int>(type: "int", nullable: false),
                    CollectionBankId = table.Column<int>(type: "int", nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollectionLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WithdrawalApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WithdrawalApplications_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WithdrawalApplications_MethodOfCollections_MethodOfCollectionId",
                        column: x => x.MethodOfCollectionId,
                        principalTable: "MethodOfCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaiverApplicationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WaiverApplicationId = table.Column<int>(type: "int", nullable: false),
                    WaiverTypeId = table.Column<int>(type: "int", nullable: false),
                    LoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaiverApplicationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WaiverApplicationDetails_WaiverApplication_WaiverApplicationId",
                        column: x => x.WaiverApplicationId,
                        principalTable: "WaiverApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WaiverApplicationDetails_WaiverTypes_WaiverTypeId",
                        column: x => x.WaiverTypeId,
                        principalTable: "WaiverTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferApplications_MemberId",
                table: "TransferApplications",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_WaiverApplication_MemberId",
                table: "WaiverApplication",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_WaiverApplication_ModeOfPaymentId",
                table: "WaiverApplication",
                column: "ModeOfPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_WaiverApplicationDetails_WaiverApplicationId",
                table: "WaiverApplicationDetails",
                column: "WaiverApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_WaiverApplicationDetails_WaiverTypeId",
                table: "WaiverApplicationDetails",
                column: "WaiverTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalApplications_MemberId",
                table: "WithdrawalApplications",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_WithdrawalApplications_MethodOfCollectionId",
                table: "WithdrawalApplications",
                column: "MethodOfCollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferApplications");

            migrationBuilder.DropTable(
                name: "WaiverApplicationDetails");

            migrationBuilder.DropTable(
                name: "WithdrawalApplications");

            migrationBuilder.DropTable(
                name: "WaiverApplication");

            migrationBuilder.DropTable(
                name: "WaiverTypes");

            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "LoanApplications");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "LoanApplications");
        }
    }
}
