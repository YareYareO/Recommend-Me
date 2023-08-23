using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecMe.Data;
using RecMe.Models;

namespace RecMe.Pages.Things
{
    public class CreateModel : PageModel
    {
        private readonly RecMe.Data.RecMeContext _context;
        public IList<Tag> Tag { get; set; } = default!;
        [BindProperty]
        public List<int>? ChosenTags { get; set; }
        public CreateModel(RecMe.Data.RecMeContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Tag = await _context.Tag.ToListAsync();
        }

        [BindProperty]
        public Thing Thing { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Thing == null || Thing == null || ChosenTags == null)
            {
                return Page();
            }
            Thing.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Thing.Add(Thing);
            await _context.SaveChangesAsync();

            ChosenTags.Add(1);
            foreach (var TagId in ChosenTags)
            {
                _context.ThingHasTag.Add(new ThingHasTag(Thing.Id, TagId));
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
