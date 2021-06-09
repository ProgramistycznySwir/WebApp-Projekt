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

        public bool? positiveVote { get; private set; }
        // Dwie w³aœciwoœci pomagaj¹ce w wyœwietlaniu przycisków do g³osowania w odpowiednim kolorze.
        public string PlusVoteButtonClass => positiveVote is true ? "btn-success" : "btn-secondary";
        public string MinusVoteButtonClass => positiveVote is false ? "btn-danger" : "btn-secondary";

        public bool isAddedToFavourites { get; private set; }


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

            isAddedToFavourites = _context.Favourites.Find(przepis.ID, currentUserID) is not null;

            return Page();
        }

        public IActionResult OnPostVote(string value, string recipeID, string voteIsPositive)
        {
            // Retrieving pseudo-session data:
            string currentUserID = _userManager.GetUserId(this.User);
            // TODO: Zaimplementowa� bardziej przyjazn� u�ytkownikowi implementacj� przekierowywania do zalogowania.
            if (currentUserID is null)
                return RedirectToPage("/Index");
                //return RedirectToPage("/Identity/Account/Login");

            var recipeID_parsed = int.Parse(recipeID);
            przepis = _context.Recipes.Find(recipeID_parsed);


            //bool temp;
            //positiveVote = bool.TryParse(voteIsPositive, out temp) ? temp : null;
            positiveVote = _context.Ratings.Find(przepis.ID, currentUserID)?.IsPositive;

            bool? voteIsPositive_parsed = value is "+" ? true : value is "-" ? false : null;
            
            if (voteIsPositive_parsed.HasValue)
                Vote(currentUserID, voteIsPositive_parsed);

            // Setting values for rendering:
            positiveVote = _context.Ratings.Find(przepis.ID, currentUserID)?.IsPositive;
            isAddedToFavourites = _context.Favourites.Find(przepis.ID, currentUserID) is not null;

            return Page();
        }

        /// <summary>
        /// Sets vote on current recipe to specified value.
        /// </summary>
        /// <param name="voteIsPositive"></param>
        /// <returns>True on success</returns>
        public void Vote(string currentUserID, bool? voteIsPositive)
        {
            // There is no vote yet.
            if (positiveVote is null)
            {
                przepis.SummaryRating += voteIsPositive is true ? 1 : -1;
                _context.Ratings.Add(new(przepis.ID, currentUserID, (bool)voteIsPositive));
            }
            // User cancelled vote.
            else if (positiveVote == voteIsPositive)
            {
                przepis.SummaryRating += voteIsPositive is true ? -1 : 1;
                // Remove rating from table.
                _context.Ratings.Remove(
                    _context.Ratings.Find(przepis.ID, currentUserID));
            }
            // User reversed vote.
            else
            {
                przepis.SummaryRating += voteIsPositive is true ? 2 : -2;
                // Reverse rating.
                var ratingToUpdate = _context.Ratings.Find(przepis.ID, currentUserID);
                ratingToUpdate.IsPositive = voteIsPositive.Value;
                _context.Ratings.Update(ratingToUpdate);
            }

            // Update recipe score.
            _context.Recipes.Update(przepis);
            _context.SaveChanges();
        }

        public IActionResult OnPostAddToFavourites(string value, string recipeID)
        {
            // Retrieving pseudo-session data:
            string currentUserID = _userManager.GetUserId(this.User);
            // TODO: Zaimplementowaæ bardziej przyjazn¹ u¿ytkownikowi implementacjê przekierowywania do zalogowania.
            if (currentUserID is null)
                return RedirectToPage("/Index");
                //return RedirectToPage("/Identity/Account/Login");

            var recipeID_parsed = int.Parse(recipeID);
            przepis = _context.Recipes.Find(recipeID_parsed);


            var favourite = _context.Favourites.Find(przepis.ID, currentUserID);
            if (favourite is null)
                _context.Favourites.Add(new(przepis.ID, currentUserID));
            else
                _context.Favourites.Remove(favourite);
            _context.SaveChanges();


            // Setting values for rendering:
            positiveVote = _context.Ratings.Find(przepis.ID, currentUserID)?.IsPositive;
            isAddedToFavourites = _context.Favourites.Find(przepis.ID, currentUserID) is not null;
            return Page();
        }
    }
}
