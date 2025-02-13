using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class LogisticRegressionSndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EndPoint",
                table: "LogisticRegressions",
                newName: "DatasetName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DatasetName",
                table: "LogisticRegressions",
                newName: "EndPoint");
        }
    }
}
