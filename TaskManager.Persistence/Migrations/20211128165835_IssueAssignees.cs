using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Persistence.Migrations
{
    public partial class IssueAssignees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Employees_AssigneeId",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "Issues");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                table: "Issues",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IssueAssignee",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueId = table.Column<long>(nullable: true),
                    AssigneeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueAssignee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueAssignee_Employees_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueAssignee_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "IssueId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_EmployeeId",
                table: "Issues",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueAssignee_AssigneeId",
                table: "IssueAssignee",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueAssignee_IssueId",
                table: "IssueAssignee",
                column: "IssueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Employees_EmployeeId",
                table: "Issues",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Employees_EmployeeId",
                table: "Issues");

            migrationBuilder.DropTable(
                name: "IssueAssignee");

            migrationBuilder.DropIndex(
                name: "IX_Issues_EmployeeId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Issues");

            migrationBuilder.AddColumn<long>(
                name: "AssigneeId",
                table: "Issues",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssigneeId",
                table: "Issues",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Employees_AssigneeId",
                table: "Issues",
                column: "AssigneeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }
    }
}
