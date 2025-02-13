using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class SndUpdateDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ColumnDescriptions",
                table: "ColumnDescriptions");

            migrationBuilder.RenameTable(
                name: "ColumnDescriptions",
                newName: "EntityDescriptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityDescriptions",
                table: "EntityDescriptions",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityDescriptions",
                table: "EntityDescriptions");

            migrationBuilder.RenameTable(
                name: "EntityDescriptions",
                newName: "ColumnDescriptions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ColumnDescriptions",
                table: "ColumnDescriptions",
                column: "Id");
        }
    }
}
