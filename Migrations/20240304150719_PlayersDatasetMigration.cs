using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class PlayersDatasetMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayersDatasets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FthPlayer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SndPlayer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FthStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SndStrategy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FthPreference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SndPreference = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersDatasets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayersDatasets");
        }
    }
}
