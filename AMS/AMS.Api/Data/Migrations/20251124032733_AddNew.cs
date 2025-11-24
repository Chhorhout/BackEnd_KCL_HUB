using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceParts_MaintenaceRequestPartId",
                table: "MaintenanceParts",
                column: "MaintenaceRequestPartId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceParts_MaintenanceRecords_MaintenaceRequestPartId",
                table: "MaintenanceParts",
                column: "MaintenaceRequestPartId",
                principalTable: "MaintenanceRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceParts_MaintenanceRecords_MaintenaceRequestPartId",
                table: "MaintenanceParts");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceParts_MaintenaceRequestPartId",
                table: "MaintenanceParts");
        }
    }
}
