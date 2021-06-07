using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt_Przepisy.Pages
{
    public class Profil_NotFoundModel : PageModel
    {
        [BindProperty]
        public int id { get; set; }

        public void OnGet()
        {
            id = Convert.ToInt32(Request.Query["id"]);
        }
    }
}
