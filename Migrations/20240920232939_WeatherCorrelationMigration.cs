using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class WeatherCorrelationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherCorrelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    record_precipitation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    average_precipitation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    actual_precipitation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    record_max_temp_year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    record_min_temp_year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    record_max_temp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    record_min_temp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    average_max_temp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    average_min_temp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    actual_max_temp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    actual_min_temp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    actual_mean_temp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherCorrelations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherCorrelations");
        }
    }
}
