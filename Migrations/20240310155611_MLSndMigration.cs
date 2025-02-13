using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class MLSndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Information",
                table: "Infos",
                newName: "NonNullCount");

            migrationBuilder.AddColumn<string>(
                name: "ColumnName",
                table: "Infos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Dtype",
                table: "Infos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColumnName",
                table: "Infos");

            migrationBuilder.DropColumn(
                name: "Dtype",
                table: "Infos");

            migrationBuilder.RenameColumn(
                name: "NonNullCount",
                table: "Infos",
                newName: "Information");
        }
    }
}
