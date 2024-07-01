using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectLex.InventoryManagement.Database.Migrations
{
    public partial class AddCarriersRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CarriersView",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CarriersEdit",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CarriersDelete",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarriersDelete",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CarriersEdit",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CarriersView",
                table: "Roles");
        }
    }
}
