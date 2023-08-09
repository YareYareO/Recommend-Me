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

        private static string[]? ChosenTagsArray; //used to preserve chosen tags
        public int PageSize { get; set; } = 10; // Number of items per page
        public int CurrentPage { get; set; } = 1; // Current page number
        public int TotalItems { get; set; } // Total number of items


        public IndexModel(RecMe.Data.RecMeContext context)
        {
            _context = context;
            querier = new ThingQuerier(context);
        }
        public async Task OnGetAsync(int currentPage = 1)
        {
            Tag = await _context.Tag.ToListAsync();
            CurrentPage = currentPage;
            int skipItems = (CurrentPage - 1) * PageSize;

            if (ChosenTagsArray != null && ChosenTagsArray.Length > 0) //if the user searched something
            {
                var things = await querier.GetThingsByTags(ChosenTagsArray).ToListAsync();
                TotalItems = things.Count;
                
                var thingsWithUpvoteCounts = things.Select(t => new
                {
                    Thing = t,
                    UpvoteCount = _context.Upvote.Count(u => u.ThingId == t.Id)
                }).ToList();

                Thing = thingsWithUpvoteCounts.OrderByDescending(t => t.UpvoteCount)
                                                    .Select(t => t.Thing)
                                                    .Skip(skipItems)
                                                    .Take(PageSize)
                                                    .ToList();
            }
            else
            {
                var thingsWithUpvoteCounts = await _context.Thing
                .Select(t => new
                {
                    Thing = t,
                    UpvoteCount = _context.Upvote.Count(u => u.ThingId == t.Id)
                })
                .ToListAsync();
                TotalItems = thingsWithUpvoteCounts.Count;

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
                return Page();
            }

            if (ChosenTags != null)
            {
                ChosenTagsArray = ChosenTags.ToArray();
            }
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostUpvoteAsync(int thingId, int currentPage)
        {
            Debug.WriteLine("----------------------------------------------------------");
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
            return RedirectToPage("./Index", new { CurrentPage = currentPage });
        }

        public async Task<int> GetTotalUpvotes(int thingId)
        {
            return await _context.Upvote.CountAsync(u => u.ThingId == thingId);
        }
    }
}
