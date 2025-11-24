using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerTypeId",
                table: "Owners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MaintainerTypeId",
                table: "Maintainers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "AssetOwnerShips",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Owners_OwnerTypeId",
                table: "Owners",
                column: "OwnerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintainers_MaintainerTypeId",
                table: "Maintainers",
                column: "MaintainerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetOwnerShips_OwnerId",
                table: "AssetOwnerShips",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetOwnerShips_Owners_OwnerId",
                table: "AssetOwnerShips",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintainers_MaintainerTypes_MaintainerTypeId",
                table: "Maintainers",
                column: "MaintainerTypeId",
                principalTable: "MaintainerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_OwnerTypes_OwnerTypeId",
                table: "Owners",
                column: "OwnerTypeId",
                principalTable: "OwnerTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetOwnerShips_Owners_OwnerId",
                table: "AssetOwnerShips");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintainers_MaintainerTypes_MaintainerTypeId",
                table: "Maintainers");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_OwnerTypes_OwnerTypeId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Owners_OwnerTypeId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Maintainers_MaintainerTypeId",
                table: "Maintainers");

            migrationBuilder.DropIndex(
                name: "IX_AssetOwnerShips_OwnerId",
                table: "AssetOwnerShips");

            migrationBuilder.DropColumn(
                name: "OwnerTypeId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "MaintainerTypeId",
                table: "Maintainers");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "AssetOwnerShips");
        }
    }
}
