using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class SentimentPlotFthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainCenturies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearCluster = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COUNTYears = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCenturies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentimentPlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Encoding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Positive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Negative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Neutral = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Compound = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentimentPlots", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainCenturies");

            migrationBuilder.DropTable(
                name: "SentimentPlots");
        }
    }
}
