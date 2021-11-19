namespace Projekt_Przepisy.Models
{
    public class RecipeIngredient
    {
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
        public int IngredientID { get; set; }
        public Ingredient Ingredient { get; set; }
        public float amount { get; set; }
    }
}