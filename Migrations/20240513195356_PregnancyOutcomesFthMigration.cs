using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class PregnancyOutcomesFthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PregnancyOutcomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    States = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Districts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnyPregnancyComplication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnyDeliveryComplication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnyPostDeliveryComplication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProblemOfVaginalDischargeDuringLastThreeMonths = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenstrualRelatedProblemsDuringLastThreeMonths = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PregnancyOutcomes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PregnancyOutcomes");
        }
    }
}
