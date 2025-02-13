using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class PlasmaFrequenciesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlasmaFrequencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterstellarMedium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IonosphericPlasma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LowestFrequencyInInterstellarMedium = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LowestFrequencyInIonosphericPlasma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlasmaFrequencies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlasmaFrequencies");
        }
    }
}
