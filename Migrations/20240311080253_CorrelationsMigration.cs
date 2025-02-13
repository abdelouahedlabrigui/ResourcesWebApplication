using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class CorrelationsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Correlations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvgAreaIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvgAreaHouseAge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvgAreaNumberofRooms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvgAreaNumberofBedrooms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaPopulation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Correlations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Correlations");
        }
    }
}
