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
        [KeyAttribute]
        public uint ID { get; set; }
        
        // <FK>
        [MaxLength(450)]
        public string UserID { get; set; }

        [MaxLength(64, ErrorMessage = "Recipe name cannot be longer than 64 characters.")]
        public string RecipeName { get; set; }
        public string IngredientsList { get; set; }
        public string InstructionsText { get; set; }
        [UrlAttribute]
        public string ImageLink { get; set; }
        public DateTime PublicationDate { get; set; }
        public int SummaryRating { get; set; }

        //TODO: Link everything with relations using FluentAPI.
        //       Useful link: https://www.tutorialspoint.com/entity_framework/entity_framework_fluent_api.htm
        // public virtual ICollection<RecipeAssignedCategory> AssignedCategories { get; set; }
    }
}
