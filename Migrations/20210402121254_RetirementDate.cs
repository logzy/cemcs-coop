using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace COOP.Banking.Migrations
{
    public partial class RetirementDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RetirementDate",
                table: "Members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Retireeexport",
                columns: table => new
                {
                    EMPNO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PINNO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYEENO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JOBTITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DIVISIONNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SURNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIRSTNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MIDDLENAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAILADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ALTEMAILADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DIRECTPHONE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ALTPHONE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LOCATIONNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RETIREMENTDATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMPLOYMENTDATE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NATIONALITY = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retireeexport", x => x.EMPNO);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Retireeexport");

            migrationBuilder.DropColumn(
                name: "RetirementDate",
                table: "Members");
        }
    }
}
