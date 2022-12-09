using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterandoRelacionamentoCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryCategory");

            migrationBuilder.AddColumn<int>(
                name: "ParentCategoryId",
                table: "Category",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "ParentCategoryId",
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
    }
}
