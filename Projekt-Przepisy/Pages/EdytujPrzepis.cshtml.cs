using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
    public class EdytujPrzepisModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Nazwa dania")]
        [Required(ErrorMessage = "Przepis musi mieć nazwę!")]
        [StringLength(64, MinimumLength = 6, ErrorMessage = "Nazwa przepisu musi mieć długość od 6 do 64 znaków.")]
        public string recipeName { get; set; }
        [BindProperty]
        [Display(Name = "Składniki")]
        [Required(ErrorMessage = "Przepis musi mieć listę składników!")]
        [StringLength(1024, MinimumLength = 6, ErrorMessage = "Nazwa przepisu musi mieć długość od 6 do 1000 znaków.")]
        public string ingredientsList { get; set; }
        [BindProperty]
        [Display(Name = "Przepis")]
        [Required(ErrorMessage = "Przepis musi mieć informacje jak go wykonać!")]
        [StringLength(4000, MinimumLength = 32, ErrorMessage = "Instrukcje muszą mieć więcej niż 32 znaki i nie więcej niż 4 tysiące.")]
        public string instructionsText { get; set; }
        [BindProperty]
        [Display(Name = "Lista Kategorii")]
        [StringLength(512, ErrorMessage = "Lista kategorii nie może być dłuższa niż 512 znaków.")]
        public string categoriesList { get; set; }

        readonly ILogger<IndexModel> _logger;
        readonly ApplicationDbContext _context;
        readonly UserManager<IdentityUser> _userManager;

        public Recipe przepis { get; private set; }

        public EdytujPrzepisModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }


        public IActionResult OnGet()
        {
            int recipeID = int.Parse(Request.Query["ID"]);
            przepis = _context.Recipes.Find(recipeID);

            if (przepis is null)
                return RedirectToPage($"/Przepis_NotFound", new { id = recipeID });

            string currentUserID = _userManager.GetUserId(this.User);
           
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Recipe recipe = _context.Recipes.First(r => r.IngredientsList == ingredientsList);
            recipe.RecipeName = recipeName;
            recipe.IngredientsList = ingredientsList;
            recipe.InstructionsText = instructionsText;

            _context.Recipes.Update(recipe);
            _context.SaveChanges();

            return RedirectToPage("/Profil");
        }
    }
}
