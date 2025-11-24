using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Relationship04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssetStatusHistoryId",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AssetStatusHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetStatusHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaintainerTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintainerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaintenaceParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenaceParts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OwnerTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryUsedRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryUsedRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetStatusHistoryId",
                table: "Assets",
                column: "AssetStatusHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AssetStatusHistories_AssetStatusHistoryId",
                table: "Assets",
                column: "AssetStatusHistoryId",
                principalTable: "AssetStatusHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssetStatusHistories_AssetStatusHistoryId",
                table: "Assets");

            migrationBuilder.DropTable(
                name: "AssetStatusHistories");

            migrationBuilder.DropTable(
                name: "MaintainerTypes");

            migrationBuilder.DropTable(
                name: "MaintenaceParts");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "OwnerTypes");

            migrationBuilder.DropTable(
                name: "TemporaryUsedRequests");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AssetStatusHistoryId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AssetStatusHistoryId",
                table: "Assets");
        }
    }
}
