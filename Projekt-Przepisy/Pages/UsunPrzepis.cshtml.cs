using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projekt_Przepisy.Data;
using Projekt_Przepisy.Models;

namespace Projekt_Przepisy.Pages
{
    public class UsunPrzepisModel : PageModel
    {
        [BindProperty]
        public int id { get; set; }
        public Recipe recipe { get; set; }

        readonly ILogger<IndexModel> _logger;
        readonly ApplicationDbContext _context;
        readonly UserManager<IdentityUser> _userManager;

        public UsunPrzepisModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            id = Convert.ToInt32(Request.Query["id"]);
            recipe = _context.Recipes.Find(id);
            // Check if user can delete recipe.
            if (recipe is null 
                || recipe.UserID != _userManager.GetUserId(this.User))
                return RedirectToPage($"/Przepis_NotFound", new { id= id });


            return Page();
        }

        public IActionResult OnPost()
        {
            recipe = _context.Recipes.Find(id);
            if (recipe is null
                || recipe.UserID != _userManager.GetUserId(this.User))
                return RedirectToPage($"/Przepis_NotFound", new { id = id });

            var assignedCategories = from assignedCat in _context.RecipeAssignedCategories
                                     where assignedCat.RecipeID == recipe.ID
                                     select assignedCat;
            // Clear assigned categories:
            foreach (var assignedCategory in assignedCategories)
            {
                var category = _context.Categories.Find(assignedCategory.CategoryID);
                if (--category.AssignedRecipesCount <= 0)
                    _context.Categories.Remove(category);
                else
                    _context.Update(category);

                _context.RecipeAssignedCategories.Remove(assignedCategory);
            }

            // Clear assigned ratings:
            var ratings = from rating in _context.Ratings
                          where rating.RecipeID == recipe.ID
                          select rating;
            _context.Ratings.RemoveRange(ratings);

            _context.Recipes.Remove(recipe);
            _context.SaveChanges();

            return RedirectToPage("/Profil");
        }
    }
}
