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
