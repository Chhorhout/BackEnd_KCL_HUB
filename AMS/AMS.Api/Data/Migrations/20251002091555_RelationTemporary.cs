using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebApi.Migrations
{
    /// <inheritdoc />
    public partial class RelationTemporary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssetStatusHistories_AssetStatusHistoryId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_TemporaryUsedRequests_TemporaryUsedRequestId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenaceParts_MaintainerRecords_MaintainerRecordId",
                table: "MaintenaceParts");

            migrationBuilder.DropTable(
                name: "MaintainerRecords");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AssetStatusHistoryId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_TemporaryUsedRequestId",
                table: "Assets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenaceParts",
                table: "MaintenaceParts");

            migrationBuilder.DropIndex(
                name: "IX_MaintenaceParts_MaintainerRecordId",
                table: "MaintenaceParts");

            migrationBuilder.DropColumn(
                name: "AssetStatusHistoryId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "MaintainerRecordId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "TemporaryUsedRequestId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "MaintainerRecordId",
                table: "MaintenaceParts");

            migrationBuilder.RenameTable(
                name: "MaintenaceParts",
                newName: "MaintenanceParts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TemporaryUsedRequests",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TemporaryUsedRequests",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "TemporaryUsedRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TemporaryUsedRecordId",
                table: "TemporaryUsedRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OwnerTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Owners",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Owners",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "AssetStatusHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceParts",
                table: "MaintenanceParts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MaintenanceRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaintainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceRecords_Maintainers_MaintainerId",
                        column: x => x.MaintainerId,
                        principalTable: "Maintainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemporaryUsedRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemporaryUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemporaryUsedRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemporaryUsedRecords_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemporaryUsedRecords_TemporaryUsers_TemporaryUserId",
                        column: x => x.TemporaryUserId,
                        principalTable: "TemporaryUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryUsedRequests_AssetId",
                table: "TemporaryUsedRequests",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryUsedRequests_TemporaryUsedRecordId",
                table: "TemporaryUsedRequests",
                column: "TemporaryUsedRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetStatusHistories_AssetId",
                table: "AssetStatusHistories",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_AssetId",
                table: "MaintenanceRecords",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRecords_MaintainerId",
                table: "MaintenanceRecords",
                column: "MaintainerId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryUsedRecords_AssetId",
                table: "TemporaryUsedRecords",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_TemporaryUsedRecords_TemporaryUserId",
                table: "TemporaryUsedRecords",
                column: "TemporaryUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetStatusHistories_Assets_AssetId",
                table: "AssetStatusHistories",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemporaryUsedRequests_Assets_AssetId",
                table: "TemporaryUsedRequests",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TemporaryUsedRequests_TemporaryUsedRecords_TemporaryUsedRecordId",
                table: "TemporaryUsedRequests",
                column: "TemporaryUsedRecordId",
                principalTable: "TemporaryUsedRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetStatusHistories_Assets_AssetId",
                table: "AssetStatusHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_TemporaryUsedRequests_Assets_AssetId",
                table: "TemporaryUsedRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_TemporaryUsedRequests_TemporaryUsedRecords_TemporaryUsedRecordId",
                table: "TemporaryUsedRequests");

            migrationBuilder.DropTable(
                name: "MaintenanceRecords");

            migrationBuilder.DropTable(
                name: "TemporaryUsedRecords");

            migrationBuilder.DropTable(
                name: "TemporaryUsers");

            migrationBuilder.DropIndex(
                name: "IX_TemporaryUsedRequests_AssetId",
                table: "TemporaryUsedRequests");

            migrationBuilder.DropIndex(
                name: "IX_TemporaryUsedRequests_TemporaryUsedRecordId",
                table: "TemporaryUsedRequests");

            migrationBuilder.DropIndex(
                name: "IX_AssetStatusHistories_AssetId",
                table: "AssetStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceParts",
                table: "MaintenanceParts");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "TemporaryUsedRequests");

            migrationBuilder.DropColumn(
                name: "TemporaryUsedRecordId",
                table: "TemporaryUsedRequests");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "AssetStatusHistories");

            migrationBuilder.RenameTable(
                name: "MaintenanceParts",
                newName: "MaintenaceParts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TemporaryUsedRequests",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TemporaryUsedRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "OwnerTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Owners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssetStatusHistoryId",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MaintainerRecordId",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TemporaryUsedRequestId",
                table: "Assets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MaintainerRecordId",
                table: "MaintenaceParts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenaceParts",
                table: "MaintenaceParts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MaintainerRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaintainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintainerRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintainerRecords_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintainerRecords_Maintainers_MaintainerId",
                        column: x => x.MaintainerId,
                        principalTable: "Maintainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetStatusHistoryId",
                table: "Assets",
                column: "AssetStatusHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_TemporaryUsedRequestId",
                table: "Assets",
                column: "TemporaryUsedRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenaceParts_MaintainerRecordId",
                table: "MaintenaceParts",
                column: "MaintainerRecordId");

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
                name: "FK_Assets_AssetStatusHistories_AssetStatusHistoryId",
                table: "Assets",
                column: "AssetStatusHistoryId",
                principalTable: "AssetStatusHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_TemporaryUsedRequests_TemporaryUsedRequestId",
                table: "Assets",
                column: "TemporaryUsedRequestId",
                principalTable: "TemporaryUsedRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenaceParts_MaintainerRecords_MaintainerRecordId",
                table: "MaintenaceParts",
                column: "MaintainerRecordId",
                principalTable: "MaintainerRecords",
                principalColumn: "Id");
        }
    }
}
