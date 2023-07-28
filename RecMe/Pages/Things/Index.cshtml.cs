using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using RecMe.Controllers.SearchThings;
using RecMe.Data;
using RecMe.Models;

namespace RecMe.Pages.Things
{
    public class IndexModel : PageModel
    {
        private readonly ThingQuerier querier;
        public IList<Thing> Thing { get; set; } = default!;
        public IList<Tag> Tag { get; set; } = default!;
        [BindProperty]
        public List<string>? ChosenTags { get; set; } //used to bind to ui checkboxes

        private static string[]? ChosenTagsArray; //used to preserve chosen tags
        

        public IndexModel(RecMe.Data.RecMeContext context)
        {
            querier = new ThingQuerier(context);
        }
        public async Task OnGetAsync()
        {   
            var tags = querier.GetAllTags();
            Tag = tags.ToList();

            if (ChosenTagsArray != null && ChosenTagsArray.Length > 0)
            {
                var things = querier.GetThingsByTags(ChosenTagsArray);
                Thing = await things.ToListAsync();
            }
            else
            {
                var things = querier.GetAllThings();
                Thing = await things.ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ChosenTags != null)
            {
                ChosenTagsArray = ChosenTags.ToArray();
            }
            return RedirectToPage("./Index");
        }
    }
}
