using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ProjectLex.InventoryManagement.Database.Migrations
{
    public partial class AddCarriers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    CarrierID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarrierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarrierStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.CarrierID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carriers");
        }
    }
}
