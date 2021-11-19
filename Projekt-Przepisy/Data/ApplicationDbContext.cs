using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Projekt_Przepisy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projekt_Przepisy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<FavouriteRecipe> Favourites { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeAssignedCategory> RecipeAssignedCategories { get; set; }
        public DbSet<RecipeCategory> Categories { get; set; }
        public DbSet<RecipeRating> Ratings { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<FavouriteRecipe>().HasKey("RecipeID", "UserID");
            builder.Entity<FavouriteRecipe>().HasKey(p => new { p.RecipeID, p.UserID });
            //builder.Entity<RecipeAssignedCategory>().HasKey("RecipeID", "ID");
            builder.Entity<RecipeAssignedCategory>().HasKey(p => new { p.RecipeID, p.ID });
            //builder.Entity<RecipeRating>().HasKey("RecipeID", "UserID");
            builder.Entity<RecipeRating>().HasKey(p => new { p.RecipeID, p.UserID });

            builder.Entity<RecipeIngredient>()
                    .HasKey(p => new { p.RecipeID, p.IngredientID })
                    .HasName("IngredientList");
            builder.Entity<Recipe>()
                    .HasMany<RecipeIngredient>()
                    .WithOne()
                    .HasForeignKey(p => p.RecipeID);
            builder.Entity<Ingredient>()
                    .HasMany<RecipeIngredient>()
                    .WithOne()
                    .HasForeignKey(p => p.IngredientID);

            builder.Entity<RecipeAuthor>()
                    .HasKey(p => new { p.RecipeID, p.AuthorID })
                    .HasName("Authors");
            builder.Entity<RecipeAuthor>()
                    .HasOne<AppUser>()
                    .WithMany()
                    .HasForeignKey(p => p.AuthorID);
            builder.Entity<Recipe>()
                    .HasMany<RecipeAuthor>()
                    .WithOne()
                    .HasForeignKey(p => p.RecipeID);
            builder.Entity<AppUser>()
                    .HasMany<Recipe>()
                    .WithOne()
                    .HasForeignKey(p => p.UserID);
        }
    }
}
