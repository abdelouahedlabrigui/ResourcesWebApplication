﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class NetworkAutomationConnectionsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NetworkAutomationConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Protocol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Command = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAT = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetworkAutomationConnections", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NetworkAutomationConnections");
        }
    }
}
