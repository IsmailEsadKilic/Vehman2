using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehman2.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOwner23091510564633 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "AppOwners",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppOwners_CompanyId",
                table: "AppOwners",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOwners_AppCompanies_CompanyId",
                table: "AppOwners",
                column: "CompanyId",
                principalTable: "AppCompanies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOwners_AppCompanies_CompanyId",
                table: "AppOwners");

            migrationBuilder.DropIndex(
                name: "IX_AppOwners_CompanyId",
                table: "AppOwners");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AppOwners");
        }
    }
}
