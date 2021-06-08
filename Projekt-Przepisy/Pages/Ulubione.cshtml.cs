using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projekt_Przepisy.Data;
using Projekt_Przepisy.Models;

namespace Projekt_Przepisy.Pages
{
    [Authorize]
    public class UlubioneModel : PageModel
    {
        public List<Recipe> favouriteRecipes { get; set; }


        readonly ILogger<IndexModel> _logger;
        readonly ApplicationDbContext _context;
        readonly UserManager<IdentityUser> _userManager;

        public UlubioneModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public void OnGet()
        {
            string currentUserID = _userManager.GetUserId(this.User);
            IQueryable<FavouriteRecipe> userFavourites = _context.Favourites
                .Where(recipe => recipe.UserID == currentUserID);
            favouriteRecipes = _context.Recipes
                .Where(recipe => userFavourites.Any(
                    favourite => favourite.RecipeID == recipe.ID))
                /*.Take(UserRecipesMaxLenght)*/.ToList();
            int a = 0;
        }
    }
}
