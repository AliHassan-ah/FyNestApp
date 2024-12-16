using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudAppProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Categories",
                newName: "CategoryName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "CompanyName");
        }
    }
}
