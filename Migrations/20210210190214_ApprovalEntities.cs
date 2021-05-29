using Microsoft.EntityFrameworkCore.Migrations;

namespace COOP.Banking.Migrations
{
    public partial class ApprovalEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleApprovers_Approvers_ApproverId",
                table: "ModuleApprovers");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingApprovals_Approvers_ApproverId",
                table: "PendingApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingApprovals_Modules_ModuleId",
                table: "PendingApprovals");

            migrationBuilder.DropIndex(
                name: "IX_PendingApprovals_ApproverId",
                table: "PendingApprovals");

            migrationBuilder.DropIndex(
                name: "IX_ModuleApprovers_ApproverId",
                table: "ModuleApprovers");

            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "PendingApprovals");

            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "ModuleApprovers");

            migrationBuilder.RenameColumn(
                name: "ModuleId",
                table: "PendingApprovals",
                newName: "ModuleApproverId");

            migrationBuilder.RenameIndex(
                name: "IX_PendingApprovals_ModuleId",
                table: "PendingApprovals",
                newName: "IX_PendingApprovals_ModuleApproverId");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "PendingApprovals",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ApprovalCount",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Members",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ModuleApproverNameStore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleApproverId = table.Column<int>(type: "int", nullable: false),
                    Usernames = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleApproverNameStore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModuleApproverNameStore_ModuleApprovers_ModuleApproverId",
                        column: x => x.ModuleApproverId,
                        principalTable: "ModuleApprovers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModuleApproverNameStore_ModuleApproverId",
                table: "ModuleApproverNameStore",
                column: "ModuleApproverId");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingApprovals_ModuleApprovers_ModuleApproverId",
                table: "PendingApprovals",
                column: "ModuleApproverId",
                principalTable: "ModuleApprovers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingApprovals_ModuleApprovers_ModuleApproverId",
                table: "PendingApprovals");

            migrationBuilder.DropTable(
                name: "ModuleApproverNameStore");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "PendingApprovals");

            migrationBuilder.DropColumn(
                name: "ApprovalCount",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "ModuleApproverId",
                table: "PendingApprovals",
                newName: "ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_PendingApprovals_ModuleApproverId",
                table: "PendingApprovals",
                newName: "IX_PendingApprovals_ModuleId");

            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "PendingApprovals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApproverId",
                table: "ModuleApprovers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PendingApprovals_ApproverId",
                table: "PendingApprovals",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_ModuleApprovers_ApproverId",
                table: "ModuleApprovers",
                column: "ApproverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleApprovers_Approvers_ApproverId",
                table: "ModuleApprovers",
                column: "ApproverId",
                principalTable: "Approvers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingApprovals_Approvers_ApproverId",
                table: "PendingApprovals",
                column: "ApproverId",
                principalTable: "Approvers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingApprovals_Modules_ModuleId",
                table: "PendingApprovals",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
