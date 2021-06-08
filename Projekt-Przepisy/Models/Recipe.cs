using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Przepisy.Models
{
    /// <summary>
    /// Entity of table Recipes.
    /// </summary>
    public class Recipe
    {
        [Key]
        //[Microsoft.EntityFrameworkCore.Metadata.ValueGenerated.]
        public int ID { get; set; }
        
        // <FK>
        [MaxLength(450)]
        public string UserID { get; set; }

        [MaxLength(64, ErrorMessage = "Recipe name cannot be longer than 64 characters.")]
        [Required]
        public string RecipeName { get; set; }
        [Required]
        public string IngredientsList { get; set; }
        [Required]
        public string InstructionsText { get; set; }
        [Url]
        public string ImageLink { get; set; }
        public DateTime PublicationDate { get; set; }
        public int SummaryRating { get; set; }

        public string LinkToPage => $"Przepis?id={ID}";

        //TODO: Link everything with relations using FluentAPI.
        //       Useful link: https://www.tutorialspoint.com/entity_framework/entity_framework_fluent_api.htm
        // public virtual ICollection<RecipeAssignedCategory> AssignedCategories { get; set; }


        //public Recipe(Data.ApplicationDbContext applicationDb)
        //{
        //    ID = (uint)applicationDb.Recipes.LongCount();
        //    ImageLink = null;
        //    PublicationDate = DateTime.Now;
        //    SummaryRating = 0;
        //}

        public Recipe(Data.ApplicationDbContext applicationDb, string userID, string recipeName,
            string ingredientsList, string instructionsText, string imageLink= null)
        {
            //ID = applicationDb.Recipes.Count();

            //Recipe();
            UserID = userID;
            RecipeName = recipeName;
            IngredientsList = ingredientsList;
            InstructionsText = instructionsText;
            ImageLink = imageLink;

            PublicationDate = DateTime.Now;
            SummaryRating = 0;
        }

        //public bool IsFavourite(Data.ApplicationDbContext context, string userID)
        //{
        //    return 
        //}

        public override string ToString()
            => $"(RecipeID: {ID},  PublicationDate: {PublicationDate},  RecipeName: {RecipeName}, " +
            $" UserID: {UserID},  SummaryRating: {SummaryRating})";
    }
}
