using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrigindoOrtografioNoCampoAltura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Heigth",
                table: "Product",
                newName: "Height");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Product",
                newName: "Heigth");
        }
    }
}
