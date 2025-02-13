using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class FinanceFthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Information_Ex1s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chapitre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExempleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entreprise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Elements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marchandises = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatieresPremieres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProduitFinis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Information_Ex1s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TravauxAFaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Chapitre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExempleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AFaire = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravauxAFaires", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Information_Ex1s");

            migrationBuilder.DropTable(
                name: "TravauxAFaires");
        }
    }
}
