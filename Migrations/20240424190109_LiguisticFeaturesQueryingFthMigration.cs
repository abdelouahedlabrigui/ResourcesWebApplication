using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class LiguisticFeaturesQueryingFthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartChat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndChar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalTrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Lefts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Rights = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ancestors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalTrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NLPSentences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sentence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Positive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Negative = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Neutral = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Compound = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NLPSentences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NounChunks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RootText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RootDep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RootHead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NounChunks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParseTrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadPos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Child = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParseTrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartOfSpeech",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatasetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text_ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lemma_ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pos_ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tag_ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dep_ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Shape_ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is_Alpha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Is_Stop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartOfSpeech", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "LocalTrees");

            migrationBuilder.DropTable(
                name: "NLPSentences");

            migrationBuilder.DropTable(
                name: "NounChunks");

            migrationBuilder.DropTable(
                name: "ParseTrees");

            migrationBuilder.DropTable(
                name: "PartOfSpeech");
        }
    }
}
