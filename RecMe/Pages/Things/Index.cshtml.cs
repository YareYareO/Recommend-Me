using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using RecMe.Controllers.SearchThings;
using RecMe.Data;
using RecMe.Models;

namespace RecMe.Pages.Things
{
    public class IndexModel : PageModel
    {
        private readonly RecMeContext _context;
        private readonly ThingQuerier querier;
        public IList<Thing> Thing { get; set; } = default!;
        public IList<Tag> Tag { get; set; } = default!;
        [BindProperty]
        public List<string>? ChosenTags { get; set; } //used to bind to ui checkboxes

        private static string[]? ChosenTagsArray; //used to preserve chosen tags for the postupvote method, because unlike in onpostasync, the chosentags get lost in the upvote method idk
        [BindProperty]
        public string SortBy { get; set; }
        public int PageSize { get; set; } = 10; // Number of items per page
        public int CurrentPage { get; set; } = 1; // Current page number
        public int TotalItems { get; set; } // Total number of items


        public IndexModel(RecMe.Data.RecMeContext context)
        {
            _context = context;
            querier = new ThingQuerier(context);
        }
        public async Task OnGetAsync(int currentPage = 1, List<string>? chosenTags = null, string sortBy = "Upvotes")
        {
            SortBy = sortBy;
            Tag = await _context.Tag.ToListAsync();
            ChosenTags = chosenTags;
            ChosenTagsArray = ChosenTags.ToArray(); //Just used to preserve chosentag info when upvoting, refreshing etc.

            CurrentPage = currentPage;
            int skipItems = (CurrentPage - 1) * PageSize;

            
            List<Thing> things;
            if (ChosenTags != null && ChosenTags.Count > 0)
            {
                things = await querier.GetThingsByTags(ChosenTags.ToArray()).ToListAsync();
            }
            else
            {
                things = await _context.Thing.ToListAsync();
            }
            TotalItems = things.Count;

            SortAndSetThings(things, skipItems);
        }

        private void SortAndSetThings(List<Thing> things, int skipItems)
        {
            var thingsWithUpvoteCounts = things.Select(t => new
            {
                Thing = t,
                UpvoteCount = _context.Upvote.Count(u => u.ThingId == t.Id)
            }).ToList();

            if (SortBy.Equals("New"))
            {
                Thing = thingsWithUpvoteCounts.OrderBy(t => t.Thing.CreatedAt)
                                                .Select(t => t.Thing)
                                                .Skip(skipItems)
                                                .Take(PageSize)
                                                .ToList();
            }
            else // Sorted by Upvotes or anything else
            {
                Thing = thingsWithUpvoteCounts.OrderByDescending(t => t.UpvoteCount)
                                                .Select(t => t.Thing)
                                                .Skip(skipItems)
                                                .Take(PageSize)
                                                .ToList();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Index", new { CurrentPage, ChosenTags, SortBy });
            }
            return RedirectToPage("./Index", new { CurrentPage, ChosenTags, SortBy });
        }

        public async Task<IActionResult> OnPostUpvoteAsync(int thingId, int currentPage)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Check if the user has already upvoted the Thing
            var existingUpvote = await _context.Upvote
                .FirstOrDefaultAsync(u => u.UserId == userId && u.ThingId == thingId);

            if (existingUpvote == null)
            {
                // User has not upvoted this Thing, create a new Upvote record
                var upvote = new Upvote
                {
                    UserId = userId,
                    ThingId = thingId
                };

                _context.Upvote.Add(upvote);
                await _context.SaveChangesAsync();
            }
            
            // Redirect or return to the page displaying the Thing details
            return RedirectToPage("./Index", new { CurrentPage = currentPage, ChosenTags = ChosenTagsArray.ToList(), SortBy});
        }

        public async Task<int> GetTotalUpvotes(int thingId)
        {
            return await _context.Upvote.CountAsync(u => u.ThingId == thingId);
        }
    }
}
