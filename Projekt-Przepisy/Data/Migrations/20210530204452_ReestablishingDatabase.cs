using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt_Przepisy.Data.Migrations
{
    public partial class ReestablishingDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    RecipeID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => new { x.RecipeID, x.UserID });
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RecipeID = table.Column<int>(nullable: false),
                    UserID = table.Column<string>(maxLength: 450, nullable: false),
                    IsPositive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.RecipeID, x.UserID });
                });

            migrationBuilder.CreateTable(
                name: "RecipeAssignedCategories",
                columns: table => new
                {
                    RecipeID = table.Column<int>(nullable: false),
                    ID = table.Column<byte>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeAssignedCategories", x => new { x.RecipeID, x.ID });
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(maxLength: 450, nullable: true),
                    RecipeName = table.Column<string>(maxLength: 64, nullable: false),
                    IngredientsList = table.Column<string>(nullable: false),
                    InstructionsText = table.Column<string>(nullable: false),
                    ImageLink = table.Column<string>(nullable: true),
                    PublicationDate = table.Column<DateTime>(nullable: false),
                    SummaryRating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RecipeAssignedCategories");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
