using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoRelacionamentoCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "SuperId",
                table: "Category");

            migrationBuilder.CreateTable(
                name: "CategoryCategory",
                columns: table => new
                {
                    ChildCategoriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    ParentCategoriesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryCategory", x => new { x.ChildCategoriesId, x.ParentCategoriesId });
                    table.ForeignKey(
                        name: "FK_CategoryCategory_Category_ChildCategoriesId",
                        column: x => x.ChildCategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryCategory_Category_ParentCategoriesId",
                        column: x => x.ParentCategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryCategory_ParentCategoriesId",
                table: "CategoryCategory",
                column: "ParentCategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryCategory");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Category",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuperId",
                table: "Category",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
