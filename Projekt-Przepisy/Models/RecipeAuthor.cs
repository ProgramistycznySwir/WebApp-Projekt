namespace Projekt_Przepisy.Models
{
    public class RecipeAuthor
    {
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
        public string AuthorID { get; set; }
        public AppUser Author { get; set; }
    }
}