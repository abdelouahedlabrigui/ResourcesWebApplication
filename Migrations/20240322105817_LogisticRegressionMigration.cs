using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class LogisticRegressionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinearRegressions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    coefficientEndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pairplotEndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    plotByColumnEndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    xlabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ylabel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinearRegressions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogisticRegressions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndPoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeValues = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropColumn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropColumns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DummyColumns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetColumn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgePClassColumns = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogisticRegressions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinearRegressions");

            migrationBuilder.DropTable(
                name: "LogisticRegressions");
        }
    }
}
