using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt_Przepisy.Data.Migrations
{
    public partial class ModifiedRecipeCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedRecipesCount",
                table: "Categories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedRecipesCount",
                table: "Categories");
        }
    }
}
