using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecMe.Data;
using RecMe.Models;

namespace RecMe.Areas.Identity.Pages.Account.Manage
{
    public class UsersThingsListModel : PageModel
    {
        private readonly RecMe.Data.RecMeContext _context;

        public UsersThingsListModel(RecMe.Data.RecMeContext context)
        {
            _context = context;
        }

        public IList<Thing> Thing { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Thing != null)
            {
                string currentuserid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Thing = await _context.Thing.Where(thing => thing.UserId == currentuserid).ToListAsync();
            }
        }
    }
}
