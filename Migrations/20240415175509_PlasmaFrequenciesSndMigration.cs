using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class PlasmaFrequenciesSndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlasmaFrequencyInInterstellarMedium",
                table: "PlasmaFrequencies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlasmaFrequencyInIonosphericPlasma",
                table: "PlasmaFrequencies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlasmaFrequencyInInterstellarMedium",
                table: "PlasmaFrequencies");

            migrationBuilder.DropColumn(
                name: "PlasmaFrequencyInIonosphericPlasma",
                table: "PlasmaFrequencies");
        }
    }
}
