using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekt_Przepisy.Data.Migrations
{
    public partial class DatabaseExpansion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IngredientsList",
                table: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RecipeAuthor",
                columns: table => new
                {
                    RecipeID = table.Column<int>(nullable: false),
                    AuthorID = table.Column<string>(nullable: false),
                    RecipeID1 = table.Column<int>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Authors", x => new { x.RecipeID, x.AuthorID });
                    table.ForeignKey(
                        name: "FK_RecipeAuthor_AspNetUsers_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeAuthor_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeAuthor_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeAuthor_Recipes_RecipeID1",
                        column: x => x.RecipeID1,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeImage",
                columns: table => new
                {
                    RecipeID = table.Column<int>(nullable: false),
                    ImageID = table.Column<byte>(nullable: false),
                    RecipeID1 = table.Column<int>(nullable: true),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Images", x => new { x.RecipeID, x.ImageID });
                    table.ForeignKey(
                        name: "FK_RecipeImage_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeImage_Recipes_RecipeID1",
                        column: x => x.RecipeID1,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeID = table.Column<int>(nullable: false),
                    IngredientID = table.Column<int>(nullable: false),
                    RecipeID1 = table.Column<int>(nullable: true),
                    IngredientID1 = table.Column<int>(nullable: true),
                    amount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("IngredientList", x => new { x.RecipeID, x.IngredientID });
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientID1",
                        column: x => x.IngredientID1,
                        principalTable: "Ingredients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeID1",
                        column: x => x.RecipeID1,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AppUserId",
                table: "Recipes",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_AppUserId1",
                table: "Recipes",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserID",
                table: "Recipes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AppUserId",
                table: "AspNetUsers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeAuthor_AuthorID",
                table: "RecipeAuthor",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeAuthor_AuthorId",
                table: "RecipeAuthor",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeAuthor_RecipeID1",
                table: "RecipeAuthor",
                column: "RecipeID1");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeImage_RecipeID1",
                table: "RecipeImage",
                column: "RecipeID1");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientID",
                table: "RecipeIngredients",
                column: "IngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientID1",
                table: "RecipeIngredients",
                column: "IngredientID1");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeID1",
                table: "RecipeIngredients",
                column: "RecipeID1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_AppUserId",
                table: "AspNetUsers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId",
                table: "Recipes",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId1",
                table: "Recipes",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_UserID",
                table: "Recipes",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_AppUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_AppUserId1",
                table: "Recipes");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_UserID",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "RecipeAuthor");

            migrationBuilder.DropTable(
                name: "RecipeImage");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AppUserId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_AppUserId1",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_UserID",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AppUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "IngredientsList",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
