using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_TechFix.Migrations
{
    /// <inheritdoc />
    public partial class AddRfq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Suppliers_SupplierID",
                table: "Quotes");

            migrationBuilder.AddColumn<string>(
                name: "QuoteStatus",
                table: "Quotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RfqID",
                table: "Quotes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuoteID",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuoteID1",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rfqs",
                columns: table => new
                {
                    RfqID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TechFixUserID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rfqs", x => x.RfqID);
                    table.ForeignKey(
                        name: "FK_Rfqs_AspNetUsers_TechFixUserID",
                        column: x => x.TechFixUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rfqs_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RfqItems",
                columns: table => new
                {
                    RfqItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RfqID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    RequestedQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RfqItems", x => x.RfqItemID);
                    table.ForeignKey(
                        name: "FK_RfqItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RfqItems_Rfqs_RfqID",
                        column: x => x.RfqID,
                        principalTable: "Rfqs",
                        principalColumn: "RfqID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quotes_RfqID",
                table: "Quotes",
                column: "RfqID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_QuoteID",
                table: "Orders",
                column: "QuoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_QuoteID1",
                table: "Orders",
                column: "QuoteID1");

            migrationBuilder.CreateIndex(
                name: "IX_RfqItems_ProductID",
                table: "RfqItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_RfqItems_RfqID",
                table: "RfqItems",
                column: "RfqID");

            migrationBuilder.CreateIndex(
                name: "IX_Rfqs_SupplierID",
                table: "Rfqs",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Rfqs_TechFixUserID",
                table: "Rfqs",
                column: "TechFixUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Quotes_QuoteID",
                table: "Orders",
                column: "QuoteID",
                principalTable: "Quotes",
                principalColumn: "QuoteID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Quotes_QuoteID1",
                table: "Orders",
                column: "QuoteID1",
                principalTable: "Quotes",
                principalColumn: "QuoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Rfqs_RfqID",
                table: "Quotes",
                column: "RfqID",
                principalTable: "Rfqs",
                principalColumn: "RfqID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Suppliers_SupplierID",
                table: "Quotes",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Quotes_QuoteID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Quotes_QuoteID1",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Rfqs_RfqID",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Suppliers_SupplierID",
                table: "Quotes");

            migrationBuilder.DropTable(
                name: "RfqItems");

            migrationBuilder.DropTable(
                name: "Rfqs");

            migrationBuilder.DropIndex(
                name: "IX_Quotes_RfqID",
                table: "Quotes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_QuoteID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_QuoteID1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QuoteStatus",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "RfqID",
                table: "Quotes");

            migrationBuilder.DropColumn(
                name: "QuoteID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "QuoteID1",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Suppliers_SupplierID",
                table: "Quotes",
                column: "SupplierID",
                principalTable: "Suppliers",
                principalColumn: "SupplierID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
