using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecMe.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RecMe.Data
{
    public class RecMeContext : IdentityDbContext
    {
        public RecMeContext (DbContextOptions<RecMeContext> options)
            : base(options)
        {
        }

        public DbSet<RecMe.Models.Thing> Thing { get; set; } = default!;

        public DbSet<RecMe.Models.ThingHasTag> ThingHasTag { get; set; } = default!;

        public DbSet<RecMe.Models.Tag> Tag { get; set; } = default!;
    }
}
