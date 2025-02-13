using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class SVMBestHyperParameterMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SVMBestHyperParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    C = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gamma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    kernel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SVMBestHyperParameters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SVMBestHyperParameters");
        }
    }
}
