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
    public class ProfilUżytkownikaModel : PageModel
    {
        ILogger<ProfilUżytkownikaModel> _logger;
        ApplicationDbContext _context;
        UserManager<IdentityUser> _userManager;

        public const int UserRecipesMaxLenght = 10;
        public List<Recipe> userRecipes { get; private set; }

        public ProfilUżytkownikaModel(ILogger<ProfilUżytkownikaModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }


        public void OnGet()
        {
            string currentUserID = Request.Query["IDUżytkownika"];
            userRecipes = _context.Recipes
                .Where(recipe => recipe.UserID == currentUserID)
                .OrderByDescending(recipe => recipe.PublicationDate)
                .Take(UserRecipesMaxLenght).ToList();
            //?? new List<Recipe>();
        }
    }
}
