using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Part6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MaintainerRecordId",
                table: "MaintenaceParts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaintenaceRequestPartId",
                table: "MaintenaceParts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MaintenaceParts_MaintainerRecordId",
                table: "MaintenaceParts",
                column: "MaintainerRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenaceParts_MaintainerRecords_MaintainerRecordId",
                table: "MaintenaceParts",
                column: "MaintainerRecordId",
                principalTable: "MaintainerRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenaceParts_MaintainerRecords_MaintainerRecordId",
                table: "MaintenaceParts");

            migrationBuilder.DropIndex(
                name: "IX_MaintenaceParts_MaintainerRecordId",
                table: "MaintenaceParts");

            migrationBuilder.DropColumn(
                name: "MaintainerRecordId",
                table: "MaintenaceParts");

            migrationBuilder.DropColumn(
                name: "MaintenaceRequestPartId",
                table: "MaintenaceParts");
        }
    }
}
