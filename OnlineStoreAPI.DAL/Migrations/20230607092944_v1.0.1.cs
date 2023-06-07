using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineStoreAPI.DAL.Migrations
{
    /// <inheritdoc />
    public partial class v101 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemCategoryItemProperty",
                columns: table => new
                {
                    ItemCategoryId = table.Column<int>(type: "integer", nullable: false),
                    ItemPropertyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategoryItemProperty", x => new { x.ItemCategoryId, x.ItemPropertyId });
                    table.ForeignKey(
                        name: "FK_ItemCategoryItemProperty_ItemCategories_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "ItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemCategoryItemProperty_ItemPropertis_ItemPropertyId",
                        column: x => x.ItemPropertyId,
                        principalTable: "ItemPropertis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCategoryItemProperty_ItemPropertyId",
                table: "ItemCategoryItemProperty",
                column: "ItemPropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCategoryItemProperty");
        }
    }
}
