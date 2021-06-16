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
    [Authorize]
    public class DodajPrzepisModel : PageModel
    {
        [BindProperty]
        [Display(Name = "Nazwa dania")]
        [Required(ErrorMessage = "Przepis musi mieæ nazwê!")]
        [StringLength(64, MinimumLength= 6, ErrorMessage= "Nazwa przepisu musi mieæ d³ugoœæ od 6 do 64 znaków.")]
        public string recipeName { get; set; }
        [BindProperty]
        [Display(Name = "Sk³adniki")]
        [Required(ErrorMessage = "Przepis musi mieæ listê sk³adników!")]
        [StringLength(1024, MinimumLength = 6, ErrorMessage = "Nazwa przepisu musi mieæ d³ugoœæ od 6 do 1000 znaków.")]
        public string ingredientsList { get; set; }
        [BindProperty]
        [Display(Name = "Przepis")]
        [Required(ErrorMessage = "Przepis musi mieæ informacje jak go wykonaæ!")]
        [StringLength(4000, MinimumLength = 32, ErrorMessage = "Instrukcje musz¹ mieæ wiêcej ni¿ 32 znaki i nie wiêcej ni¿ 4 tysi¹ce.")]
        public string instructionsText { get; set; }
        [BindProperty]
        [Display(Name = "Lista Kategorii")]
        [StringLength(512, ErrorMessage = "Lista kategorii nie mo¿e byæ d³u¿sza ni¿ 512 znaków.")]
        public string categoriesList { get; set; }
        //[Display(Name = "Przepis")]
        //[Required(ErrorMessage = "Przepis musi mieæ nazwê!")]
        //[StringLength(64, MinimumLength = 6, ErrorMessage = "Nazwa przepisu musi mieæ d³ugoœæ od 6 do 64.")]
        //public string recipeName { get; set; }


        readonly ILogger<IndexModel> _logger;
        readonly ApplicationDbContext _context;
        readonly UserManager<IdentityUser> _userManager;

        public DodajPrzepisModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
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
            if (!ModelState.IsValid)
                return Page();

            Recipe newRecipe = new(_context,
                recipeName: recipeName,
                ingredientsList: ingredientsList,
                instructionsText: instructionsText,
                userID: _userManager.GetUserId(this.User)
                //TODO: Implement image links propperly
                //imageLink: null
                );
            //TODO: Bardzo du¿o _context.SaveChanges(), ale nie chce mi siê myœleæ nad tym jak to obejœæ, a dzia³a :)
            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();
            newRecipe = _context.Recipes.First(r => r.IngredientsList == ingredientsList);

            foreach (var categoryName in categoriesList.Split(' ').Select(str => str.ToLower()))
            {
                var category = _context.Categories.FirstOrDefault(cat => cat.Name == categoryName);
                if (category is null)
                {
                    _context.Categories.Add(new RecipeCategory(categoryName));
                    _context.SaveChanges();
                    category = _context.Categories.First(cat => cat.Name == categoryName);
                }
                else
                {
                    category.AssignedRecipesCount++;
                    _context.Categories.Update(category);
                }

                var recipeAssignedCategory = new RecipeAssignedCategory(newRecipe.ID, category.ID);
                _context.RecipeAssignedCategories.Add(recipeAssignedCategory);
                _context.SaveChanges();
            }

            return RedirectToPage("/Profil");
        }
    }
}
