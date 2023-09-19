using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehman2.Migrations
{
    /// <inheritdoc />
    public partial class AddedVehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppVehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Plate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarModelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FuelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppVehicles_AppCarModels_CarModelId",
                        column: x => x.CarModelId,
                        principalTable: "AppCarModels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppVehicles_AppFuels_FuelId",
                        column: x => x.FuelId,
                        principalTable: "AppFuels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppVehicles_AppOwners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AppOwners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppVehicles_CarModelId",
                table: "AppVehicles",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_AppVehicles_FuelId",
                table: "AppVehicles",
                column: "FuelId");

            migrationBuilder.CreateIndex(
                name: "IX_AppVehicles_OwnerId",
                table: "AppVehicles",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppVehicles");
        }
    }
}
