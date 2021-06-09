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
    public class PrzepisModel : PageModel
    {
        readonly ILogger<IndexModel> _logger;
        readonly ApplicationDbContext _context;
        readonly UserManager<IdentityUser> _userManager;

        public Recipe przepis { get; private set; }
        

        public PrzepisModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

       
        public void OnGet()
        {
            int recipeID = Int32.Parse(Request.Query["ID"]); 


            przepis = _context.Recipes
               .Where(recipe => recipe.ID == recipeID).First();
        }
    }
}
