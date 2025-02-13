using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class AccidentsSndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccidentDateTime",
                table: "Accidents",
                newName: "AccidentDate");

            migrationBuilder.AddColumn<string>(
                name: "Time24hr",
                table: "Accidents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time24hr",
                table: "Accidents");

            migrationBuilder.RenameColumn(
                name: "AccidentDate",
                table: "Accidents",
                newName: "AccidentDateTime");
        }
    }
}
