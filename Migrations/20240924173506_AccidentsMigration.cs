using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class AccidentsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accidents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GridRefEasting = table.Column<int>(type: "int", nullable: false),
                    GridRefNorthing = table.Column<int>(type: "int", nullable: false),
                    NumberOfVehicles = table.Column<int>(type: "int", nullable: false),
                    AccidentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstRoadClass = table.Column<int>(type: "int", nullable: false),
                    FirstRoadClassAndNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoadSurface = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LightingConditions = table.Column<int>(type: "int", nullable: false),
                    WeatherConditions = table.Column<int>(type: "int", nullable: false),
                    LocalAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNumber = table.Column<int>(type: "int", nullable: false),
                    TypeOfVehicle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CasualtyReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CasualtyVehicleNumber = table.Column<int>(type: "int", nullable: false),
                    CasualtyClass = table.Column<int>(type: "int", nullable: false),
                    CasualtySeverity = table.Column<int>(type: "int", nullable: false),
                    SexOfCasualty = table.Column<int>(type: "int", nullable: false),
                    AgeOfCasualty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accidents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accidents");
        }
    }
}
