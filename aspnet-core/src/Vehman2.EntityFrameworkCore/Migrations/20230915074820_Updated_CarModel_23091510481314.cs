using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehman2.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCarModel23091510481314 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrandId",
                table: "AppCarModels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppCarModels_BrandId",
                table: "AppCarModels",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCarModels_AppBrands_BrandId",
                table: "AppCarModels",
                column: "BrandId",
                principalTable: "AppBrands",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCarModels_AppBrands_BrandId",
                table: "AppCarModels");

            migrationBuilder.DropIndex(
                name: "IX_AppCarModels_BrandId",
                table: "AppCarModels");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "AppCarModels");
        }
    }
}
