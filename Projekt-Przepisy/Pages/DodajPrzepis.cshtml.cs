using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
        [StringLength(1024, MinimumLength = 6, ErrorMessage = "Nazwa przepisu musi mie� d�ugo�� od 6 do 1000 znak�w.")]
        public string ingredientsList { get; set; }
        [BindProperty]
        [Display(Name = "Przepis")]
        [Required(ErrorMessage = "Przepis musi mie� informacje jak go wykona�!")]
        [StringLength(4000, MinimumLength = 32, ErrorMessage = "Instrukcje musz� mie� wi�cej ni� 32 znaki i nie wi�cej ni� 4 tysi�ce.")]
        public string instructionsText { get; set; }
        [BindProperty]
        [Display(Name = "Lista Kategorii")]
        [StringLength(512, ErrorMessage = "Lista kategorii nie mo�e by� d�u�sza ni� 512 znak�w.")]
        public string categoriesList { get; set; }
        //[Display(Name = "Przepis")]
        //[Required(ErrorMessage = "Przepis musi mie� nazw�!")]
        //[StringLength(64, MinimumLength = 6, ErrorMessage = "Nazwa przepisu musi mie� d�ugo�� od 6 do 64.")]
        //public string recipeName { get; set; }
        public string imageLink { get; set; }
       



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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AccessRights,DOB,FirstName,LastName,NRIC,Nationality,StaffIdentity")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {

                        var file = Image;
                        var uploads = Path.Combine("uploads\\img\\employees" , Image.FileName);

                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse
                                (file.ContentDisposition).FileName.Trim('"');

                            System.Console.WriteLine(fileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                recipe.ImageLink = file.FileName;
                            }


                        }
                    }
                }
            }
        }    

                public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

                Recipe newRecipe = new(_context,
                recipeName: recipeName,
                ingredientsList: ingredientsList,
                instructionsText: instructionsText,
                userID: _userManager.GetUserId(this.User),
                //TODO: Implement image links propperly
                imageLink: null
                );
            //TODO: Bardzo du�o _context.SaveChanges(), ale nie chce mi si� my�le� nad tym jak to obej��, a dzia�a :)
            _context.Recipes.Add(newRecipe);
            _context.SaveChanges();
            newRecipe = _context.Recipes.First(r => r.IngredientsList == ingredientsList);

            if(categoriesList is null)
                return RedirectToPage("/Profil");
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

                var recipeAssignedCategory = new RecipeAssignedCategory(_context, newRecipe.ID, category.ID);
                _context.RecipeAssignedCategories.Add(recipeAssignedCategory);
                _context.SaveChanges();
            }

            return RedirectToPage("/Profil");
        }
    }
}
