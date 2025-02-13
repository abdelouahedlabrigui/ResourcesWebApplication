using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class BankruptcySndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sentence",
                table: "BankruptcyPartOfSpeech");

            migrationBuilder.DropColumn(
                name: "Sentence",
                table: "BankruptcyParseTrees");

            migrationBuilder.DropColumn(
                name: "Sentence",
                table: "BankruptcyNounChunks");

            migrationBuilder.DropColumn(
                name: "Sentence",
                table: "BankruptcyNLPSentences");

            migrationBuilder.DropColumn(
                name: "Sentence",
                table: "BankruptcyLocalTrees");

            migrationBuilder.DropColumn(
                name: "Sentence",
                table: "BankruptcyEntities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sentence",
                table: "BankruptcyPartOfSpeech",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sentence",
                table: "BankruptcyParseTrees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sentence",
                table: "BankruptcyNounChunks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sentence",
                table: "BankruptcyNLPSentences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sentence",
                table: "BankruptcyLocalTrees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sentence",
                table: "BankruptcyEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
