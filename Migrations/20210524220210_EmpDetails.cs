using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class EmpDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ResignationDate",
                table: "Employees",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "EmployeeDetails",
                columns: table => new
                {
                    EMPNO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PINNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STAFF_GRP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYEE_DEPT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYEE_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TITLES = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SURNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIRSTNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTHERNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATE_BIRTH = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GENDER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MARITAL_STATUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WORK_PHONE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MOBILE_PHONE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATE_EMPLOY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL_ADDR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JOB_TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STATEORIGING = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RES_ADDR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYEE_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYEE_DEPT_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MONTH_BASIC_SAL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ANNUAL_BASIC_SAL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetails", x => x.EMPNO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ResignationDate",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
