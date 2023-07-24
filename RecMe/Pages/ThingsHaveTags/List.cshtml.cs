using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecMe.Data;
using RecMe.Models;

namespace RecMe.Pages.ThingsHaveTags
{
    public class ListModel : PageModel
    {
        private readonly RecMe.Data.RecMeContext _context;

        public ListModel(RecMe.Data.RecMeContext context)
        {
            _context = context;
        }

        public IList<ThingHasTag> ThingHasTag { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.ThingHasTag != null)
            {
                ThingHasTag = await _context.ThingHasTag.ToListAsync();
            }
        }
    }
}
