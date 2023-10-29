using RecMe.Models;
using System.Linq;
using System.Diagnostics;

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
            return _context.Thing;
        }


        private int _skipItems = 0;
        private int _itemsPerPage = 20; // This exists in Things/Index.cshtml.cs aswell, so it is repeated. (Oh nooo) if the items per page count becomes changable, figure out a solution
        public IQueryable<Thing> GetSortedThings(List<string> chosentags, int currentPage = 1, string sortBy="Upvotes")
        {
            _skipItems = (currentPage - 1) * _itemsPerPage;

            IQueryable<Thing> query = SortThings(GetThingsByTags(chosentags), sortBy);
            return query;
        }
        private IQueryable<Thing> GetThingsByTags(IEnumerable<string> tagnames)
        {
            var tagIds = _context.Tag
                .Where(tag => tagnames.Contains(tag.Name))
                .Select(tag => tag.Id);

            var things = _context.ThingHasTag
                .Where(tht => tagIds.Contains(tht.TagId))
                .GroupBy(tht => tht.ThingId)
                .Where(group => group.Count() == tagnames.Count())
                .Select(group => group.Key);

            return _context.Thing.Where(thing => things.Contains(thing.Id));
        }
        private IQueryable<Thing> SortThings(IQueryable<Thing> things, string sortBy)
        {
            Dictionary<string, Func<IQueryable<Thing>, IQueryable<Thing>>> sortingoptions = new Dictionary<string, Func<IQueryable<Thing>, IQueryable<Thing>>>();
            sortingoptions["Upvotes"] = (t) => SortByUpvotes(t);
            sortingoptions["New"] = (t) => SortByNew(t);

            if(sortingoptions.ContainsKey(sortBy))
            {
                things = sortingoptions[sortBy](things);
            }

            return things;
        }

        private IQueryable<Thing> SortByNew(IQueryable<Thing> things)
        {
            return things.OrderByDescending(t => t.CreatedAt)
                                                .Select(t => t)
                                                .Skip(_skipItems)
                                                .Take(_itemsPerPage);
        }
        private IQueryable<Thing> SortByUpvotes(IQueryable<Thing> things)
        {
            return things.Select(t => new
            {
                Thing = t,
                UpvoteCount = _context.Upvote.Count(u => u.ThingId == t.Id)  //cant use upvote querier because weird "cant read as sql" error
            })
                .OrderByDescending(t => t.UpvoteCount)
                                                .Select(t => t.Thing)
                                                .Skip(_skipItems)
                                                .Take(_itemsPerPage);
        }
        /* old implementation
         public IQueryable<Thing> GetThingsByTags(List<string> tagnames)
        {
            var things = from thing in GetAllThings()
                         from tag in _context.Tag
                         from tht in _context.ThingHasTag
                         where tht.TagId == tag.Id && tagnames.Contains(tag.Name) && thing.Id == tht.ThingId
                         group thing by thing.Name into thingx
                         where thingx.Count() == tagnames.Count
                         select thingx.First();

            return things;
        }*/

    }
}
