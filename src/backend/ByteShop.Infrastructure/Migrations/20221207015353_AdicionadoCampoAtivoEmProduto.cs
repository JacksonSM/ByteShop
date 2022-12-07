using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoCampoAtivoEmProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParentCategoryId",
                table: "Category",
                newName: "SuperId");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Product",
                type: "nvarchar(30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Product",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MainImageUrl",
                table: "Product",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SKU",
                table: "Product",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondaryImageUrl",
                table: "Product",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Category",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SKU",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SecondaryImageUrl",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "SuperId",
                table: "Category",
                newName: "ParentCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "Brand",
                table: "Product",
                type: "nvarchar(60)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)");
        }
    }
}
