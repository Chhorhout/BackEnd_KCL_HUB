using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Part5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TemporaryUsedRequestId",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Assets_TemporaryUsedRequestId",
                table: "Assets",
                column: "TemporaryUsedRequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_TemporaryUsedRequests_TemporaryUsedRequestId",
                table: "Assets",
                column: "TemporaryUsedRequestId",
                principalTable: "TemporaryUsedRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_TemporaryUsedRequests_TemporaryUsedRequestId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_TemporaryUsedRequestId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "TemporaryUsedRequestId",
                table: "Assets");
        }
    }
}
