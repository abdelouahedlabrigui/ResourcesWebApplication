using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class AccountingTrdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Exemples",
                newName: "ObjetId");

            migrationBuilder.RenameColumn(
                name: "Instructions",
                table: "Exemples",
                newName: "InstructionId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Exemples",
                newName: "ExempleId");

            migrationBuilder.AddColumn<string>(
                name: "ConditionId",
                table: "Exemples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnonceeId",
                table: "Exemples",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ExempleSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NExemple = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExempleSeries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExempleSeries");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "Exemples");

            migrationBuilder.DropColumn(
                name: "EnonceeId",
                table: "Exemples");

            migrationBuilder.RenameColumn(
                name: "ObjetId",
                table: "Exemples",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "InstructionId",
                table: "Exemples",
                newName: "Instructions");

            migrationBuilder.RenameColumn(
                name: "ExempleId",
                table: "Exemples",
                newName: "Description");
        }
    }
}
