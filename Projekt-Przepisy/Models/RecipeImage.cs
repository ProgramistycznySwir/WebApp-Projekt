using System.ComponentModel.DataAnnotations;

namespace Projekt_Przepisy.Models
{
    public class RecipeImage
    {
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
        public byte ImageID { get; set; }

        [Url]
        public string Link { get; set; }
    }
}