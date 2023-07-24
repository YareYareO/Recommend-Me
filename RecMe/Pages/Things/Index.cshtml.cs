using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecMe.Data;
using RecMe.Models;

namespace RecMe.Pages.Things
{
    public class IndexModel : PageModel
    {
        private readonly RecMe.Data.RecMeContext _context;

        public IndexModel(RecMe.Data.RecMeContext context)
        {
            _context = context;
        }

        public IList<Thing> Thing { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Thing != null)
            {
                Thing = await _context.Thing.ToListAsync();
            }
        }
    }
}
