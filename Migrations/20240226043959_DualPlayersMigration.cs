using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class DualPlayersMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DualPlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FthStrategy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SndStrategy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FthPreference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SndPreference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FthUtility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SndUtility = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DualPlayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Choice = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DualPlayers");

            migrationBuilder.DropTable(
                name: "Rules");
        }
    }
}
