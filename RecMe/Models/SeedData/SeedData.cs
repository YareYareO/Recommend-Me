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
                        Description = "Cool Manga"
                    },

                    new Thing
                    {
                        Name = "The Alchemist",
                        Description = "inspirational"
                    },

                    new Thing
                    {
                        Name = "Lord Of The Ring",
                        Description = "classic fantasy"
                    },

                    new Thing
                    {
                        Name = "A Song of Ice and Fire",
                        Description = "last season sucked"
                    },
                    new Thing
                    {
                        Name = "One Piece",
                        Description = "MINAMINOOOO SHIMAWAAAAA"
                    },

                    new Thing
                    {
                        Name = "Thriller",
                        Description = "Best selling album of all time"
                    },

                    new Thing
                    {
                        Name = "College Dropout",
                        Description = "Kanyes Debut"
                    },

                    new Thing
                    {
                        Name = "Get Rich Or Die Trying",
                        Description = "50s debut"
                    },
                    new Thing
                    {
                        Name = "Suavemente",
                        Description = "SUAVEMENTEEEEE"
                    },

                    new Thing
                    {
                        Name = "The Slim Shady LP",
                        Description = "Hi, my name is"
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
                        Name = "Book"
                    },

                    new Tag
                    {
                        Name = "Fantasy"
                    },

                    new Tag
                    {
                        Name = "Non Fiction"
                    },

                    new Tag
                    {
                        Name = "Manga"
                    },
                    new Tag
                    {
                        Name = "Album"
                    },

                    new Tag
                    {
                        Name = "Hip Hop Album"
                    },

                    new Tag
                    {
                        Name = "Eminem"
                    },

                    new Tag
                    {
                        Name = "Pop Album"
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
                        ThingId = 1,
                        TagId = 2
                    },

                    new ThingHasTag
                    {
                        ThingId = 1,
                        TagId = 4
                    },

                    new ThingHasTag
                    {
                        ThingId = 2,
                        TagId = 1
                    },
                    new ThingHasTag
                    {
                        ThingId = 3,
                        TagId = 1
                    },

                    new ThingHasTag
                    {
                        ThingId = 3,
                        TagId = 2
                    },

                    new ThingHasTag
                    {
                        ThingId = 4,
                        TagId = 1
                    },

                    new ThingHasTag
                    {
                        ThingId = 4,
                        TagId = 2
                    },
                    new ThingHasTag
                    {
                        ThingId = 5,
                        TagId = 1
                    },

                    new ThingHasTag
                    {
                        ThingId = 5,
                        TagId = 4
                    },

                    new ThingHasTag
                    {
                        ThingId = 5,
                        TagId = 2
                    },

                    new ThingHasTag
                    {
                        ThingId = 7,
                        TagId = 5
                    },
                    new ThingHasTag
                    {
                        ThingId = 7,
                        TagId = 8
                    },

                    new ThingHasTag
                    {
                        ThingId = 8,
                        TagId = 5
                    },

                    new ThingHasTag
                    {
                        ThingId = 8,
                        TagId = 6
                    },

                    new ThingHasTag
                    {
                        ThingId = 9,
                        TagId = 5
                    },
                    new ThingHasTag
                    {
                        ThingId = 9,
                        TagId = 6
                    },

                    new ThingHasTag
                    {
                        ThingId = 10,
                        TagId = 5
                    },

                    new ThingHasTag
                    {
                        ThingId = 10,
                        TagId = 8
                    },

                    new ThingHasTag
                    {
                        ThingId = 11,
                        TagId = 5
                    },
                    new ThingHasTag
                    {
                        ThingId = 11,
                        TagId = 6
                    },

                    new ThingHasTag
                    {
                        ThingId = 11,
                        TagId = 7
                    }
                );
                count++;
            }
            if(count > 0) context.SaveChanges();
        }
    }
}