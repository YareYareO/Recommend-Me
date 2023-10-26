
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecMe.Controllers.SearchThings;
using RecMe.Data;
using RecMe.Models;

namespace RecMe.Pages.Things
{
    public class IndexModel : PageModel
    {
        private readonly RecMeContext _context;
        private readonly ThingQuerier querier;
        internal readonly UpvoteQuerier upvoteQuerier;
        public IList<Thing> Thing { get; set; } = default!;
        public IList<Tag> Tag { get; set; } = default!;
        [BindProperty]
        public List<string>? ChosenTags { get; set; } //used to bind to ui checkboxes

        private static string[]? ChosenTagsArray; //used to preserve chosen tags for the postupvote method, because unlike in onpostasync, the chosentags get lost in the upvote method idk
        [BindProperty]
        public string SortBy { get; set; }
        public int ItemsPerPage { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public int TotalItems { get; set; }


        public IndexModel(RecMe.Data.RecMeContext context)
        {
            _context = context;
            querier = new ThingQuerier(context);
            upvoteQuerier = new UpvoteQuerier(context);
        }
        public async Task OnGetAsync(int currentPage = 1, List<string>? chosenTags = null, string sortBy = "Upvotes")
        {
            SortBy = sortBy;
            Tag = await _context.Tag.ToListAsync();
            ChosenTags = chosenTags;
            ChosenTagsArray = ChosenTags.ToArray(); //Just used to preserve chosentag info when upvoting, refreshing etc.

            CurrentPage = currentPage;
            int skipItems = (CurrentPage - 1) * ItemsPerPage;

            
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
                UpvoteCount = upvoteQuerier.GetAll(t.Id) //cant use the async version because of strange error
            }).ToList();

            if (SortBy.Equals("New"))
            {
                Thing = thingsWithUpvoteCounts.OrderByDescending(t => t.Thing.CreatedAt)
                                                .Select(t => t.Thing)
                                                .Skip(skipItems)
                                                .Take(ItemsPerPage)
                                                .ToList();
                return;
            }
            Thing = thingsWithUpvoteCounts.OrderByDescending(t => t.UpvoteCount)
                                                .Select(t => t.Thing)
                                                .Skip(skipItems)
                                                .Take(ItemsPerPage)
                                                .ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return RedirectToPage("./Index", new { CurrentPage, ChosenTags, SortBy });
            //}
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
    }
}
