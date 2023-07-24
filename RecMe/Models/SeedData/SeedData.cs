using Microsoft.EntityFrameworkCore;
using RecMe.Models;
using RecMe.Data;
using System.Diagnostics;

namespace RecMe.Models.SeedData;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new RecMeContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<RecMeContext>>()))
        {
            if (context == null || context.Thing == null)
            {
                throw new ArgumentNullException("Null ViewContext");
            }
            int count = 0;

            // Look for any Things.
            if (context.Thing.Any())
            {
                Debug.WriteLine("Thing not empty");
            }
            else {
                context.Thing.AddRange(
                    new Thing
                    {
                        Name = "Berserk",
                        Description = "Cooler Manga"
                    },

                    new Thing
                    {
                        Name = "IPhone 8",
                        Description = "Ein Handy"
                    },

                    new Thing
                    {
                        Name = "100 Nike Air",
                        Description = "Digga das heißt Red"
                    },

                    new Thing
                    {
                        Name = "The Eminem Show",
                        Description = "ein Klassiker"
                    }
                );
                count++;
            }

            //Look for any Tags
            if (context.Tag.Any())
            {
                Debug.WriteLine("Tag not empty");
            }
            else
            {
                context.Tag.AddRange(
                    new Tag
                    {
                        Name = "Manga"
                    },

                    new Tag
                    {
                        Name = "Handy"
                    },

                    new Tag
                    {
                        Name = "Sneaker"
                    },

                    new Tag
                    {
                        Name = "Album"
                    }
                );
                count++;
            }

            //Looking for any ThingHasTags
            if (context.ThingHasTag.Any())
            {
                Debug.WriteLine("ThingHasTag not empty");
            }
            else
            {
                context.ThingHasTag.AddRange(
                    new ThingHasTag
                    {
                        ThingId = 1,
                        TagId = 1
                    },

                    new ThingHasTag
                    {
                        ThingId = 2,
                        TagId = 2
                    },

                    new ThingHasTag
                    {
                        ThingId = 3,
                        TagId = 3
                    },

                    new ThingHasTag
                    {
                        ThingId = 4,
                        TagId = 4
                    }
                );
                count++;
            }
            if(count > 0) context.SaveChanges();
        }
    }
}