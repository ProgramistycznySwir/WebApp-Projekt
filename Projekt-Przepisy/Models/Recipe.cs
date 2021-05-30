using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Przepisy.Models
{
    /// <summary>
    /// Entity of table Recipes.
    /// </summary>
    public class Recipe
    {
        //TODO: PK place here!
        
        public string RecipeName { get; set; }
        public string IngredientsList { get; set; }
        public string InstructionText { get; set; }
        public string ImageLink { get; set; }
        public DateTime PublicationDate { get; set; }
        public int SummaryRating { get; set; }
    }
}
