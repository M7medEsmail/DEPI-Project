using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_DomainLayer.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "ProductBrands");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "ProductBrands",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
