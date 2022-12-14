using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoCampoWidthNaTabelaProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lenght",
                table: "Product");

            migrationBuilder.AddColumn<float>(
                name: "Length",
                table: "Product",
                type: "REAL",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Width",
                table: "Product",
                type: "REAL",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Length",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Product");

            migrationBuilder.AddColumn<float>(
                name: "Lenght",
                table: "Product",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
