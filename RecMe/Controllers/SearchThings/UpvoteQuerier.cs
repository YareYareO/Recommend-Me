using RecMe.Data;
using Microsoft.EntityFrameworkCore;


namespace RecMe.Controllers.SearchThings
{

    public class UpvoteQuerier
    {
        private readonly RecMeContext _context;
        public UpvoteQuerier(RecMe.Data.RecMeContext context) 
        { 
            _context = context;
        }
        public int GetAll(int thingId)
        {
            return _context.Upvote.Count(u => u.ThingId == thingId);
        }

        public async Task<int> GetAllAsync(int thingId)
        {
            return await _context.Upvote.CountAsync(u => u.ThingId == thingId);
        }
    }
}
