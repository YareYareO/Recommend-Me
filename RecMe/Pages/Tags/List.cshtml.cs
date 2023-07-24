using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecMe.Data;
using RecMe.Models;

namespace RecMe.Pages.Tags
{
    public class ListModel : PageModel
    {
        private readonly RecMe.Data.RecMeContext _context;

        public ListModel(RecMe.Data.RecMeContext context)
        {
            _context = context;
        }

        public IList<Tag> Tag { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Tag != null)
            {
                Tag = await _context.Tag.ToListAsync();
            }
        }
    }
}
