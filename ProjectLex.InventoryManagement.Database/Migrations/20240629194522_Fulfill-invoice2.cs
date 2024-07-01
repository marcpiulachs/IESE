using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectLex.InventoryManagement.Database.Migrations
{
    /// <inheritdoc />
    public partial class Fulfillinvoice2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FulFills",
                columns: table => new
                {
                    FulFillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FulFillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FulFillStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FulFills", x => new { x.FulFillID, x.OrderID });
                    table.ForeignKey(
                        name: "FK_FulFills_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK_FulFills_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FulFills_Staffs_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => new { x.InvoiceID, x.OrderID });
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Staffs_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FulFillID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FulFillOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => new { x.PackageID, x.OrderID });
                    table.ForeignKey(
                        name: "FK_Packages_FulFills_FulFillID_FulFillOrderID",
                        columns: x => new { x.FulFillID, x.FulFillOrderID },
                        principalTable: "FulFills",
                        principalColumns: new[] { "FulFillID", "OrderID" });
                    table.ForeignKey(
                        name: "FK_Packages_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FulFills_CustomerID",
                table: "FulFills",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_FulFills_OrderID",
                table: "FulFills",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_FulFills_StaffID",
                table: "FulFills",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerID",
                table: "Invoices",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderID",
                table: "Invoices",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_StaffID",
                table: "Invoices",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_FulFillID_FulFillOrderID",
                table: "Packages",
                columns: new[] { "FulFillID", "FulFillOrderID" });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_OrderID",
                table: "Packages",
                column: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "FulFills");
        }
    }
}
