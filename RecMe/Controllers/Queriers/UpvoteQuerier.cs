using RecMe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RecMe.Models;

namespace RecMe.Controllers.SearchThings
{

    public class UpvoteQuerier
    {
        private readonly RecMeContext _context;
        public UpvoteQuerier(RecMe.Data.RecMeContext context) 
        { 
            _context = context;
        }
        public int Get(int thingId)
        {
            return _context.Upvote.Count(u => u.ThingId == thingId);
        }
        public IQueryable<Upvote> GetAll()
        {
            return _context.Upvote;
        }
        public async Task AddOne(Upvote upvote)
        {
            await _context.Upvote.AddAsync(upvote);
            
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetAsync(int thingId)
        {
            return await _context.Upvote.CountAsync(u => u.ThingId == thingId);
        }
    }
}
