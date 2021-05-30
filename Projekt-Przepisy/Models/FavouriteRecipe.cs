using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Przepisy.Models
{
    /// <summary>
    /// Entity of table Favourites.
    /// </summary>
    public class FavouriteRecipe
    {
        [Key]
        public int RecipeID { get; set; }

        // <FK>
        /// <summary>
        /// User that like this recipe.
        /// </summary>
        [Key]
        [MaxLength(450)]
        public string UserID { get; set; }
    }
}
