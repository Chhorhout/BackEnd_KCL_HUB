using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationship03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaintainerRecordId",
                table: "Maintainers");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "MaintainerRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MaintainerId",
                table: "MaintainerRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MaintainerRecords_AssetId",
                table: "MaintainerRecords",
                column: "AssetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MaintainerRecords_MaintainerId",
                table: "MaintainerRecords",
                column: "MaintainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintainerRecords_Assets_AssetId",
                table: "MaintainerRecords",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintainerRecords_Maintainers_MaintainerId",
                table: "MaintainerRecords",
                column: "MaintainerId",
                principalTable: "Maintainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintainerRecords_Assets_AssetId",
                table: "MaintainerRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintainerRecords_Maintainers_MaintainerId",
                table: "MaintainerRecords");

            migrationBuilder.DropIndex(
                name: "IX_MaintainerRecords_AssetId",
                table: "MaintainerRecords");

            migrationBuilder.DropIndex(
                name: "IX_MaintainerRecords_MaintainerId",
                table: "MaintainerRecords");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "MaintainerRecords");

            migrationBuilder.DropColumn(
                name: "MaintainerId",
                table: "MaintainerRecords");

            migrationBuilder.AddColumn<Guid>(
                name: "MaintainerRecordId",
                table: "Maintainers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
