using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class Filepath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCommitteeBalances");

            migrationBuilder.DropTable(
                name: "EmployeeDetails");

            migrationBuilder.DropTable(
                name: "RetireeBalances");

            migrationBuilder.DropTable(
                name: "Retireeexport");

            migrationBuilder.AddColumn<string>(
                name: "filePath",
                table: "LoanApplications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Executives",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Executives",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "filePath",
                table: "LoanApplications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Executives",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "Executives",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CreditCommitteeBalances",
                columns: table => new
                {
                    EMPNO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Executive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HAPL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LTLoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SDEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STLoan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Savgs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TSL1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TSL2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TSL3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCommitteeBalances", x => x.EMPNO);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDetails",
                columns: table => new
                {
                    EMPNO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ANNUAL_BASIC_SAL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATE_BIRTH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATE_EMPLOY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL_ADDR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYEE_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYEE_DEPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYEE_DEPT_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYEE_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIRSTNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GENDER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JOB_TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MARITAL_STATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MOBILE_PHONE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MONTH_BASIC_SAL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTHERNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PINNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RES_ADDR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STAFF_GRP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STATEORIGING = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SURNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TITLES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WORK_PHONE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetails", x => x.EMPNO);
                });

            migrationBuilder.CreateTable(
                name: "RetireeBalances",
                columns: table => new
                {
                    EMPNO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LONGTERM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SAVINGS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SHORTTERM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SPECDEP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetireeBalances", x => x.EMPNO);
                });

            migrationBuilder.CreateTable(
                name: "Retireeexport",
                columns: table => new
                {
                    EMPNO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ALTEMAILADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ALTPHONE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DIRECTPHONE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DIVISIONNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAILADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYEENO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYMENTDATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIRSTNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JOBTITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LOCATIONNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MIDDLENAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NATIONALITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PINNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RETIREMENTDATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SURNAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retireeexport", x => x.EMPNO);
                });
        }
    }
}
