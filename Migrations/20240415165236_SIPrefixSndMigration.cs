using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class SIPrefixSndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Latin",
                table: "SIPrefixes",
                newName: "Prefix");

            migrationBuilder.RenameColumn(
                name: "Greek",
                table: "SIPrefixes",
                newName: "Factor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prefix",
                table: "SIPrefixes",
                newName: "Latin");

            migrationBuilder.RenameColumn(
                name: "Factor",
                table: "SIPrefixes",
                newName: "Greek");
        }
    }
}
