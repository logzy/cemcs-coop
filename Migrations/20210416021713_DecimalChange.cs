using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class DecimalChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlySavingsAmount",
                table: "LoanConfigs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinLoanAmount",
                table: "LoanConfigs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxLoanAmount",
                table: "LoanConfigs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LumpSumSavingsAmount",
                table: "LoanConfigs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExistingLoanFeeAmount",
                table: "LoanConfigs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CreditCommitteeBalances",
                columns: table => new
                {
                    EMPNO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Savgs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STLoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LTLoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HAPL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TSL1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TSL2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TSL3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Executive = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCommitteeBalances", x => x.EMPNO);
                });

            migrationBuilder.CreateTable(
                name: "RetireeBalances",
                columns: table => new
                {
                    EMPNO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SAVINGS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SPECDEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SHORTTERM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LONGTERM = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetireeBalances", x => x.EMPNO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCommitteeBalances");

            migrationBuilder.DropTable(
                name: "RetireeBalances");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");

            migrationBuilder.AlterColumn<decimal>(
                name: "MonthlySavingsAmount",
                table: "LoanConfigs",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinLoanAmount",
                table: "LoanConfigs",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxLoanAmount",
                table: "LoanConfigs",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "LumpSumSavingsAmount",
                table: "LoanConfigs",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExistingLoanFeeAmount",
                table: "LoanConfigs",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
