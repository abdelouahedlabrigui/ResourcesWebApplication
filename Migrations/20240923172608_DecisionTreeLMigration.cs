using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class DecisionTreeLMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BestHyperParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    max_depth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    min_samples_leaf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    min_samples_split = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestHyperParameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClassificationReportings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    _1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    _2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    _3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accuracy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MacroAvg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeightedAvg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassificationReportings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeatureImportances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Feature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Importance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureImportances", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BestHyperParameters");

            migrationBuilder.DropTable(
                name: "ClassificationReportings");

            migrationBuilder.DropTable(
                name: "FeatureImportances");
        }
    }
}
