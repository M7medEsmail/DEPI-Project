using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_DomainLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBrandAndTypeAndOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductBrandId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId1",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "ProductBrands",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "OrderItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "OrderItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductBrandId1",
                table: "Products",
                column: "ProductBrandId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId1",
                table: "Products",
                column: "ProductTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId1",
                table: "OrderItem",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_ProductId1",
                table: "OrderItem",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId1",
                table: "Products",
                column: "ProductBrandId1",
                principalTable: "ProductBrands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId1",
                table: "Products",
                column: "ProductTypeId1",
                principalTable: "ProductTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_ProductId1",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductBrands_ProductBrandId1",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductBrandId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ProductId1",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductBrandId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypeId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "ProductBrands");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "OrderItem");
        }
    }
}
