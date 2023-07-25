using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using RecMe.Controllers.SearchThings;
using RecMe.Data;
using RecMe.Models;

namespace RecMe.Pages.Things
{
    public class IndexModel : PageModel
    {
        //private readonly RecMe.Data.RecMeContext _context;
        private readonly ThingQuerier querier;

        public IndexModel(RecMe.Data.RecMeContext context)
        {
            //_context = context;
            querier = new ThingQuerier(context);
        }

        public IList<Thing> Thing { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var things = querier.GetAllThings();

            if (!string.IsNullOrEmpty(SearchString))
            {   
                things = querier.GetThingsByTag(SearchString);
            }
            Thing = await things.ToListAsync();
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
    }
}
