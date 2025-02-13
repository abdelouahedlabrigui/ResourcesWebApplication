using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class BankruptcyFstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankruptcyEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sentence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartChat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndChar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankruptcyEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankruptcyLocalTrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sentence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Lefts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Rights = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ancestors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankruptcyLocalTrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankruptcyNLPSentences",
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
                    table.PrimaryKey("PK_BankruptcyNLPSentences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankruptcyNounChunks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sentence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RootText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RootDep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RootHead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankruptcyNounChunks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankruptcyParseTrees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sentence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadPos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Child = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankruptcyParseTrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankruptcyPartOfSpeech",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sentence = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_BankruptcyPartOfSpeech", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankruptcyEntities");

            migrationBuilder.DropTable(
                name: "BankruptcyLocalTrees");

            migrationBuilder.DropTable(
                name: "BankruptcyNLPSentences");

            migrationBuilder.DropTable(
                name: "BankruptcyNounChunks");

            migrationBuilder.DropTable(
                name: "BankruptcyParseTrees");

            migrationBuilder.DropTable(
                name: "BankruptcyPartOfSpeech");
        }
    }
}
