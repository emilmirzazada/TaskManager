using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Persistence.Migrations
{
    public partial class IssueAssigneesDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueAssignee_Employees_AssigneeId",
                table: "IssueAssignee");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueAssignee_Issues_IssueId",
                table: "IssueAssignee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueAssignee",
                table: "IssueAssignee");

            migrationBuilder.RenameTable(
                name: "IssueAssignee",
                newName: "IssueAssignees");

            migrationBuilder.RenameIndex(
                name: "IX_IssueAssignee_IssueId",
                table: "IssueAssignees",
                newName: "IX_IssueAssignees_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueAssignee_AssigneeId",
                table: "IssueAssignees",
                newName: "IX_IssueAssignees_AssigneeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueAssignees",
                table: "IssueAssignees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueAssignees_Employees_AssigneeId",
                table: "IssueAssignees",
                column: "AssigneeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueAssignees_Issues_IssueId",
                table: "IssueAssignees",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "IssueId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueAssignees_Employees_AssigneeId",
                table: "IssueAssignees");

            migrationBuilder.DropForeignKey(
                name: "FK_IssueAssignees_Issues_IssueId",
                table: "IssueAssignees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IssueAssignees",
                table: "IssueAssignees");

            migrationBuilder.RenameTable(
                name: "IssueAssignees",
                newName: "IssueAssignee");

            migrationBuilder.RenameIndex(
                name: "IX_IssueAssignees_IssueId",
                table: "IssueAssignee",
                newName: "IX_IssueAssignee_IssueId");

            migrationBuilder.RenameIndex(
                name: "IX_IssueAssignees_AssigneeId",
                table: "IssueAssignee",
                newName: "IX_IssueAssignee_AssigneeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IssueAssignee",
                table: "IssueAssignee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueAssignee_Employees_AssigneeId",
                table: "IssueAssignee",
                column: "AssigneeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IssueAssignee_Issues_IssueId",
                table: "IssueAssignee",
                column: "IssueId",
                principalTable: "Issues",
                principalColumn: "IssueId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
