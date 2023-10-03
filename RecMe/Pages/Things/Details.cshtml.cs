using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecMe.Data;
using RecMe.Models;

namespace RecMe.Pages.Things
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly RecMe.Data.RecMeContext _context;

        public DetailsModel(RecMe.Data.RecMeContext context)
        {
            _context = context;
        }

      public Thing Thing { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Thing == null)
            {
                return NotFound();
            }

            var thing = await _context.Thing.FirstOrDefaultAsync(m => m.Id == id);
            if (thing == null)
            {
                return NotFound();
            }
            else 
            {
                Thing = thing;
            }
            return Page();
        }
    }
}
