using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class EntityRecognitionFthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EntityRecognitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sentence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InformationExtraction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityEncoding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosCountsEncoding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagCountsEncoding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepCountsEncoding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShapeCountsEncoding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AlphaCountsEncoding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StopCountsEncoding = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityRecognitions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityRecognitions");
        }
    }
}
