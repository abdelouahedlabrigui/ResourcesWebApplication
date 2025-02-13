using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class ServiceChecksSndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "vmUser",
                table: "ServiceChecks",
                newName: "VmUser");

            migrationBuilder.RenameColumn(
                name: "vmPassword",
                table: "ServiceChecks",
                newName: "VmPassword");

            migrationBuilder.RenameColumn(
                name: "vmIP",
                table: "ServiceChecks",
                newName: "VmIP");

            migrationBuilder.RenameColumn(
                name: "service",
                table: "ServiceChecks",
                newName: "Service");

            migrationBuilder.RenameColumn(
                name: "agent_ip",
                table: "ServiceChecks",
                newName: "Agent_ip");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VmUser",
                table: "ServiceChecks",
                newName: "vmUser");

            migrationBuilder.RenameColumn(
                name: "VmPassword",
                table: "ServiceChecks",
                newName: "vmPassword");

            migrationBuilder.RenameColumn(
                name: "VmIP",
                table: "ServiceChecks",
                newName: "vmIP");

            migrationBuilder.RenameColumn(
                name: "Service",
                table: "ServiceChecks",
                newName: "service");

            migrationBuilder.RenameColumn(
                name: "Agent_ip",
                table: "ServiceChecks",
                newName: "agent_ip");
        }
    }
}
