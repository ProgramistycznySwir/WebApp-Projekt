using System.ComponentModel.DataAnnotations;

namespace Projekt_Przepisy.Models
{
    public class Ingredient
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public IngredientAmountType Type { get; set; }

        public ICollection<RecipeIngredient> Recipes { get; set; }
    }

    public enum IngredientAmountType { Mass, Volume, Quantity }
}