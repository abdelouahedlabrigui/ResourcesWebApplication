using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class USPresidentsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USPresidents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresidentNO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    President = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Born = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartOfPresidency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndOfPresidency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostPresidency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Died = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NetWorth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoliticalParty = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USPresidents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USPresidents");
        }
    }
}
