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
        [Required(ErrorMessage = "Przepis musi mie� nazw�!")]
        [StringLength(64, MinimumLength= 6, ErrorMessage= "Nazwa przepisu musi mie� d�ugo�� od 6 do 64 znak�w.")]
        public string recipeName { get; set; }
        [BindProperty]
        [Display(Name = "Sk�adniki")]
        [Required(ErrorMessage = "Przepis musi mie� list� sk�adnik�w!")]
        //TODO: Ustawi� tu jaki� limit bo to troche nieodpowiedzialne zostawia� to w takim stanie :/
        //[StringLength(64, MinimumLength = 6, ErrorMessage = "Nazwa przepisu musi mie� d�ugo�� od 6 do 64.")]
        public string ingredientsList { get; set; }
        [BindProperty]
        [Display(Name = "Przepis")]
        [Required(ErrorMessage = "Przepis musi mie� informacje jak go wykona�!")]
        [StringLength(4000, MinimumLength = 32, ErrorMessage = "Instrukcje musz� mie� wi�cej ni� 32 znaki i nie wi�cej ni� 4 tysi�ce.")]
        public string instructionsText { get; set; }
        //[Display(Name = "Przepis")]
        //[Required(ErrorMessage = "Przepis musi mie� nazw�!")]
        //[StringLength(64, MinimumLength = 6, ErrorMessage = "Nazwa przepisu musi mie� d�ugo�� od 6 do 64.")]
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

            //newRecipe.RecipeName = recipeName;
            //newRecipe.IngredientsList = ingredientsList;
            //newRecipe.InstructionsText = instructionsText;
            //newRecipe.UserID = _userManager.GetUserId(this.User);
            //TODO: Implement image links propperly
            //newRecipe.ImageLink = null;

            //var userID = _userManager.GetUserId(this.User);
            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();

            //exampleFormControlFile1

            return Page();
        }
    }
}
