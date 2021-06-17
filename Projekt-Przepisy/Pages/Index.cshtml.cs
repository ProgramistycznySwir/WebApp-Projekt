using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projekt_Przepisy.Data;
using Projekt_Przepisy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Projekt_Przepisy.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string searchedPhrase { get; set; }
        [BindProperty]
        public string searchMode { get; set; }

        public const int SearchResultsMaxLenght = 10;
        public List<Recipe> searchResults { get; set; }

        public List<Recipe> topRecipes { get; set; }

        readonly ILogger<IndexModel> _logger;
        readonly ApplicationDbContext _context;
        readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public void OnGet()
        {
            // This one is setted only once per visiting page soo i think it's futile to move it to it's own method.
            topRecipes = _context.Recipes
                .OrderByDescending(recipe => recipe.SummaryRating)
                .Take(SearchResultsMaxLenght).ToList();
        }

        public IActionResult OnPost()
        {           
            if (string.IsNullOrWhiteSpace(searchedPhrase))
                return Page();

            // TODO: Zaimplementować lepsze wyszukiwanie!
            searchResults = (searchMode switch
            {
                "recipe" => from recipe in _context.Recipes
                            where recipe.RecipeName.Contains(searchedPhrase)
                            select recipe,
                "user" => from recipe in _context.Recipes
                          join user in _context.Users on recipe.UserID equals user.Id
                          where user.UserName.Contains(searchedPhrase)
                          select recipe,
                "category" => from recipe in _context.Recipes
                              join assignedCat in _context.RecipeAssignedCategories on recipe.ID equals assignedCat.RecipeID
                              join category in _context.Categories on assignedCat.CategoryID equals category.ID
                              where category.Name.Contains(searchedPhrase)
                              select recipe,
                _ => null
            })?.OrderByDescending(recipe => recipe.PublicationDate)
                ?.Take(SearchResultsMaxLenght)?.ToList();

            return Page();
        }
    }
}
