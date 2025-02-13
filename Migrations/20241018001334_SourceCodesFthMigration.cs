using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class SourceCodesFthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AerodynamicFileInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastWriteTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastAccessTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AerodynamicFileInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AircraftPropulsionFileInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastWriteTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastAccessTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftPropulsionFileInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AtmosphericFileInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastWriteTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastAccessTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtmosphericFileInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuantumMechanicFileInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastWriteTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastAccessTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuantumMechanicFileInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SeismologyFileInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastWriteTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastAccessTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeismologyFileInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AerodynamicFileInfos");

            migrationBuilder.DropTable(
                name: "AircraftPropulsionFileInfos");

            migrationBuilder.DropTable(
                name: "AtmosphericFileInfos");

            migrationBuilder.DropTable(
                name: "QuantumMechanicFileInfos");

            migrationBuilder.DropTable(
                name: "SeismologyFileInfos");
        }
    }
}
