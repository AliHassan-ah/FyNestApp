using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudAppProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedRatinginProductDetailtbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "ProductDetails",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "ProductDetails");
        }
    }
}
