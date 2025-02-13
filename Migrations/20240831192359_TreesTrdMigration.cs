using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class TreesTrdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "initialMetadatDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DirectoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exists = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsReadOnly = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastAccessTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastAccessTimeUtc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastWriteTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastWriteTimeUtc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_initialMetadatDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VersionInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileBuildPart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileMajorPart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileMinorPart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePrivatePart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileVersionRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InternalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDebug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPatched = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPreRelease = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPrivateBuild = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSpecialBuild = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalCopyright = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalTrademarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalFilename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivateBuild = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductBuildPart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductMajorPart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductMinorPart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductPrivatePart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductVersion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductVersionRaw = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialBuild = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VersionInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "initialMetadatDetails");

            migrationBuilder.DropTable(
                name: "VersionInfos");
        }
    }
}
