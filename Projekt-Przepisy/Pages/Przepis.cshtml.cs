using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Projekt_Przepisy.Data;
using Projekt_Przepisy.Models;


namespace Projekt_Przepisy.Pages
{
    public class PrzepisModel : PageModel
    {
       
        public void OnGet()
        {
            int recipeID = Int16.Parse(Request.Query["ID"]);


        }
    }
}
