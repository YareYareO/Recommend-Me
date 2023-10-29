
using System.Diagnostics;
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
        internal readonly ThingQuerier thingQuerier;
        internal readonly UpvoteQuerier upvoteQuerier;
        internal readonly TagQuerier tagQuerier;
        public IList<Thing> Thing { get; set; } = default!;
        public IList<Tag> Tag { get; set; } = default!;
        [BindProperty]
        public List<string>? ChosenTags { get; set; } //used to bind to ui checkboxes

        private static string[]? ChosenTagsArray; //used to preserve chosen tags for the postupvote method, because unlike in onpostasync, the chosentags get lost in the upvote method idk
        [BindProperty]
        public string? SortBy { get; set; }
        public int ItemsPerPage { get; set; } = 20;
        public int CurrentPage { get; set; } = 1;
        public int TotalItems { get; set; }


        public IndexModel(RecMe.Data.RecMeContext context)
        {
            _context = context;
            thingQuerier = new ThingQuerier(context);
            upvoteQuerier = new UpvoteQuerier(context);
            tagQuerier = new TagQuerier(context);
        }
        public async Task OnGetAsync(int currentPage = 1, List<string>? chosenTags = null, string sortBy = "Upvotes")
        {
            SortBy = sortBy;
            Tag = await tagQuerier.GetAll().ToListAsync();

            ChosenTags = chosenTags ?? new List<string>();
            ChosenTagsArray = ChosenTags.ToArray(); //Just used to preserve chosentag info when upvoting, refreshing etc.
            CurrentPage = currentPage;

            Thing = ChosenTags.Count > 0
                ? await thingQuerier.GetSortedThings(ChosenTags, currentPage, sortBy).ToListAsync()
                : new List<Thing>();

            TotalItems = Thing.Count;
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage("./Index", new { CurrentPage, ChosenTags, SortBy });
            }
            return RedirectToPage("./Index");
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
                    UserId = userId ?? "unknownForSomeReason",
                    ThingId = thingId
                };

                _context.Upvote.Add(upvote);
                await _context.SaveChangesAsync();
            }
            
            // Redirect or return to the page displaying the Thing details
            return RedirectToPage("./Index", new { CurrentPage = currentPage, ChosenTags = ChosenTagsArray?.ToList(), SortBy});
        }
    }
}
