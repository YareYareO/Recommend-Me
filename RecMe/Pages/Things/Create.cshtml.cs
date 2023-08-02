using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult OnGet()
        {
            Tag = _context.Tag.ToList();
            return Page();
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

            _context.Thing.Add(Thing);
            await _context.SaveChangesAsync();

            foreach (var TagId in ChosenTags)
            {
                _context.ThingHasTag.Add(new ThingHasTag(Thing.Id, TagId));
            }
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
