using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectLex.InventoryManagement.Database.Migrations
{
    public partial class AddCarriersRoles2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "CarriersAdd",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarriersAdd",
                table: "Roles");
        }
    }
}
