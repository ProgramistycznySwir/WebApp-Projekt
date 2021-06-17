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
        public int recipeID { get; set; }
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
            recipeID = int.Parse(Request.Query["ID"]);
            przepis = _context.Recipes.Find(recipeID);

            if (przepis is null)
                return RedirectToPage($"/Przepis_NotFound", new { id = recipeID });
            categoriesList = string.Join(" ",
                from categories in _context.Categories
                join assignedCat in _context.RecipeAssignedCategories on categories.ID equals assignedCat.CategoryID
                where assignedCat.RecipeID == przepis.ID
                select categories.Name);
           
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            //recipeID = int.Parse(Request.Query["recipeID"]);
            Recipe recipe = _context.Recipes.Find(recipeID);
            // Verify if someone is not forging html form.
            if (recipe.UserID != _userManager.GetUserId(this.User))
                return RedirectToPage("/Index");

            recipe.RecipeName = recipeName;
            recipe.IngredientsList = ingredientsList;
            recipe.InstructionsText = instructionsText;
            // Apply changes.
            _context.Recipes.Update(recipe);

            var recipeCategories = from assignedCat in _context.RecipeAssignedCategories
                                   //join category in _context.Categories on assignedCat.CategoryID equals category.ID
                                   where assignedCat.RecipeID == recipe.ID
                                   //orderby category.Name
                                   select assignedCat;
            var inputCategoriesNames = categoriesList.ToLower().Split(' ').ToHashSet();
            foreach (RecipeAssignedCategory assignedCategory in recipeCategories)
            {
                RecipeCategory category = _context.Categories.Find(assignedCategory.CategoryID);
                bool setContainsName = inputCategoriesNames.Contains(category.Name);
                inputCategoriesNames.Remove(category.Name);
                if (setContainsName)
                    // Leave as is.
                    continue;

                // Remove assigned category from db:
                if (--category.AssignedRecipesCount <= 0)
                    _context.Categories.Remove(category);
                else
                    _context.Categories.Update(category);

                _context.RecipeAssignedCategories.Remove(assignedCategory);
            }
            // Add categories to db:
            foreach(string categoryName in inputCategoriesNames)
            {
                var category = _context.Categories.FirstOrDefault(cat => cat.Name == categoryName);
                if (category is null)
                {
                    category = _context.Categories.Add(new RecipeCategory(categoryName)).Entity;
                    _context.SaveChanges();
                }
                else
                {
                    category.AssignedRecipesCount++;
                    _context.Categories.Update(category);
                }    
                _context.RecipeAssignedCategories.Add(new RecipeAssignedCategory(_context, recipeID, category.ID));
            }

            _context.SaveChanges();

            return RedirectToPage("/Profil");
        }
    }
}
