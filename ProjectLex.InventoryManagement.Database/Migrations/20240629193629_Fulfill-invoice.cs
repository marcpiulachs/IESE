using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectLex.InventoryManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class Fulfillinvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryStatus",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderFulFillStatus",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderFulFillStatus",
                table: "OrderDetails");
        }
    }
}
