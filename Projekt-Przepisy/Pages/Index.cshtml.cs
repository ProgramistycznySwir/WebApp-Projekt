using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Projekt_Przepisy.Data;
using Projekt_Przepisy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Przepisy.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string searchedPhrase { get; set; }

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
            searchResults = _context.Recipes
                .Where(recipe => recipe.RecipeName.Contains(searchedPhrase))
                .OrderByDescending(recipe => recipe.PublicationDate)
                .Take(SearchResultsMaxLenght).ToList();

            return Page();
        }
    }
}
