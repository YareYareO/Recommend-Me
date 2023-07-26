using RecMe.Models;
using System.Linq;

namespace RecMe.Controllers.SearchThings
{
    public class ThingQuerier
    {
        private readonly RecMe.Data.RecMeContext _context;
        public ThingQuerier(RecMe.Data.RecMeContext context) 
        {
            _context = context;
        }
        public IQueryable<Thing> GetAllThings()
        {
            var things = from thing in _context.Thing
                         select thing;
            return things;
        }
        public IQueryable<Thing> GetThingsByTag(string searchString)
        {   //finds things that have a tag that contains ONE searchstring
            var things = (from thing in GetAllThings()
                          from tag in _context.Tag
                          from tht in _context.ThingHasTag
                          where tht.TagId == tag.Id && tag.Name.Contains(searchString) && thing.Id == tht.ThingId
                          select thing).Distinct();
            return things;
        }
        public IQueryable<Thing> GetThingsByTags(string[] tagstrings) 
        {
            var things = from thing in this.GetAllThings()
                         from tag in _context.Tag
                         from tht in _context.ThingHasTag
                         where tht.TagId == tag.Id && tagstrings.Contains(tag.Name) && thing.Id == tht.ThingId
                         group thing by thing.Name into thingx
                         where thingx.Count() == tagstrings.Length 
                         select thingx.First();
            return things;
        }
    }
}
