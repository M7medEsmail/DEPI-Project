using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPI_DomainLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddingPicturalUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PicturalUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicturalUrl",
                table: "AspNetUsers");
        }
    }
}
