using RecMe.Models;

namespace RecMe.Controllers.SearchThings
{
    public class TagQuerier
    {
        private readonly RecMe.Data.RecMeContext _context;
        public TagQuerier(RecMe.Data.RecMeContext context)
        {
            _context = context;
        }
        public IQueryable<Tag> GetAll()
        {
            return _context.Tag;
        }
    }
}
