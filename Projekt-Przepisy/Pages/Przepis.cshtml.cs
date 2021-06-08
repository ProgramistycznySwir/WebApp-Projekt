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

        public bool? positiveVote;
        // Dwie w³aœciwoœci pomagaj¹ce w wyœwietlaniu przycisków do g³osowania w odpowiednim kolorze.
        public string PlusVoteButtonClass => positiveVote is true ? "btn-success" : "btn-secondary";
        public string MinusVoteButtonClass => positiveVote is false ? "btn-danger" : "btn-secondary";


        public PrzepisModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
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
            positiveVote = _context.Ratings.Find(recipeID, currentUserID)?.IsPositive;

            return Page();
        }

        public IActionResult OnPostVote(string value, string recipeID, string voteIsPositive)
        {
            przepis = _context.Recipes.Find(int.Parse(recipeID));
            bool temp;
            positiveVote = bool.TryParse(voteIsPositive, out temp) ? temp : null;

            bool? isPositive = value is "+" ? true : value is "-" ? false : null;
            if (isPositive is null)
                return Page();


            string currentUserID = _userManager.GetUserId(this.User);
            if (currentUserID is null)
                return RedirectToPage("/Login");
            // There is no vote yet.
            if(positiveVote is null)
            {
                przepis.SummaryRating += isPositive is true ? 1 : -1;
                _context.Ratings.Add(new(przepis.ID, currentUserID, (bool)isPositive));
            }
            // User cancelled vote.
            else if (positiveVote == isPositive)
            {
                przepis.SummaryRating += isPositive is true ? -1 : 1;
                // Remove rating from table.
                _context.Ratings.Remove(
                    _context.Ratings.Find(przepis.ID, currentUserID));
            }
            // User reversed vote.
            else
            {
                przepis.SummaryRating += isPositive is true ? 2 : -2;
                // Reverse rating.
                var ratingToUpdate = _context.Ratings.Find(przepis.ID, currentUserID);
                ratingToUpdate.IsPositive = isPositive.Value;
                _context.Ratings.Update(ratingToUpdate);
            }

            // Update recipe score.
            _context.Recipes.Update(przepis);
            _context.SaveChanges();

            // Reasign propper value for rendering.
            positiveVote = _context.Ratings.Find(przepis.ID, currentUserID)?.IsPositive;

            return Page();
        }
    }
}
