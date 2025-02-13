using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class TreesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InitialMetadata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTimeUtc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exists = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastAccessTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastAccessTimeUtc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastWriteTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastWriteTimeUtc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PSChildName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PSIsContainer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PSParentPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PSPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InitialMetadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTimeUtc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exists = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastAccessTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastAccessTimeUtc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastWriteTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastWriteTimeUtc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentParameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Root = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PSDrives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Credential = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayRoot = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaximumSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Root = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSDrives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PSProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capabilities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Drives = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HelpFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Home = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImplementingType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PSSnapIn = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Attributes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTimeUtc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exists = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastAccessTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastAccessTimeUtc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastWriteTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastWriteTimeUtc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RootProperty = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roots", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InitialMetadata");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "PSDrives");

            migrationBuilder.DropTable(
                name: "PSProviders");

            migrationBuilder.DropTable(
                name: "Roots");
        }
    }
}
