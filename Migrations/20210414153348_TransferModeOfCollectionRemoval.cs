using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class TransferModeOfCollectionRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MethodOfCollectionId",
                table: "TransferApplications");

            //migrationBuilder.AddColumn<bool>(
            //    name: "Approved",
            //    table: "LoanApplications",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "BankCode",
            //    table: "LoanApplications",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "BankName",
            //    table: "LoanApplications",
            //    type: "nvarchar(max)",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Approved",
            //    table: "LoanApplications");

            //migrationBuilder.DropColumn(
            //    name: "BankCode",
            //    table: "LoanApplications");

            //migrationBuilder.DropColumn(
            //    name: "BankName",
            //    table: "LoanApplications");

            migrationBuilder.AddColumn<int>(
                name: "MethodOfCollectionId",
                table: "TransferApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
