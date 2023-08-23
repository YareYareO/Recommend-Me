using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RecMe.Data;
using RecMe.Models;

namespace RecMe.Areas.Identity.Pages.Account.Manage
{
    public class UsersThingEditModel : PageModel
    {
        private readonly RecMe.Data.RecMeContext _context;

        public UsersThingEditModel(RecMe.Data.RecMeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Thing Thing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Thing == null)
            {
                return NotFound();
            }

            var thing =  await _context.Thing.FirstOrDefaultAsync(m => m.Id == id);
            if (thing == null)
            {
                return NotFound();
            }
            Thing = thing;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Thing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThingExists(Thing.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./UsersThingsList");
        }

        private bool ThingExists(int id)
        {
          return (_context.Thing?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
