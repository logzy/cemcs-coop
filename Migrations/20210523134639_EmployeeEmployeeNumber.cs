using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class EmployeeEmployeeNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleApproverNameStore_ModuleApprovers_ModuleApproverId",
                table: "ModuleApproverNameStore");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleApproverNameStore",
                table: "ModuleApproverNameStore");

            migrationBuilder.RenameTable(
                name: "ModuleApproverNameStore",
                newName: "ModuleApproverNameStores");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleApproverNameStore_ModuleApproverId",
                table: "ModuleApproverNameStores",
                newName: "IX_ModuleApproverNameStores_ModuleApproverId");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleApproverNameStores",
                table: "ModuleApproverNameStores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleApproverNameStores_ModuleApprovers_ModuleApproverId",
                table: "ModuleApproverNameStores",
                column: "ModuleApproverId",
                principalTable: "ModuleApprovers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleApproverNameStores_ModuleApprovers_ModuleApproverId",
                table: "ModuleApproverNameStores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModuleApproverNameStores",
                table: "ModuleApproverNameStores");

            migrationBuilder.DropColumn(
                name: "EmployeeNumber",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "ModuleApproverNameStores",
                newName: "ModuleApproverNameStore");

            migrationBuilder.RenameIndex(
                name: "IX_ModuleApproverNameStores_ModuleApproverId",
                table: "ModuleApproverNameStore",
                newName: "IX_ModuleApproverNameStore_ModuleApproverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModuleApproverNameStore",
                table: "ModuleApproverNameStore",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleApproverNameStore_ModuleApprovers_ModuleApproverId",
                table: "ModuleApproverNameStore",
                column: "ModuleApproverId",
                principalTable: "ModuleApprovers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
