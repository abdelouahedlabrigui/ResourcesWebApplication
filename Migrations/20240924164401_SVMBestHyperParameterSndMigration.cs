﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ResourcesWebApplication.Migrations
{
    public partial class SVMBestHyperParameterSndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DatasetName",
                table: "SVMBestHyperParameters",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatasetName",
                table: "SVMBestHyperParameters");
        }
    }
}
