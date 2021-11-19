using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Projekt_Przepisy.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Recipe> Recipes { get; set; }

        public ICollection<AppUser> FavouriteAuthors { get; set; }
        public ICollection<Recipe> FavouriteRecipes { get; set; }
    }
}