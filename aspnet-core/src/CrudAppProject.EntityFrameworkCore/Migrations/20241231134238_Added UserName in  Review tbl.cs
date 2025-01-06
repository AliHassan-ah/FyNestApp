using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudAppProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserNameinReviewtbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ProductReviews",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ProductReviews");
        }
    }
}
