using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class SndHashedStringsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Hash",
                table: "Hash");

            migrationBuilder.RenameTable(
                name: "Hash",
                newName: "Hashes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hashes",
                table: "Hashes",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Hashes",
                table: "Hashes");

            migrationBuilder.RenameTable(
                name: "Hashes",
                newName: "Hash");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hash",
                table: "Hash",
                column: "Id");
        }
    }
}
