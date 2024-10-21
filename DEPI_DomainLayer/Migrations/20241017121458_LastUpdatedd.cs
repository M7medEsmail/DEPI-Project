using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_DomainLayer.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdatedd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "OrderId");
        }
    }
}
