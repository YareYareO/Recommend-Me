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

           

            //Look for any Tags
            if (context.Tag.Any())
            {
                Debug.WriteLine("Tag not empty");
            }
            //All Tags (warning: long)
            else
            {
                context.Tag.AddRange(
                    //Things parents
                    new Tag
                    {
                        Name = "Thing"
                    },

                    new Tag
                    {
                        Name = "Media", //2
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Person", //3
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Art", //4
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Event", //5
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Place", //6
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Entity", //7
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Gadget", //8
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Game", //9
                        ParentId = 1
                    },
                    //media parents
                    new Tag
                    {
                        Name = "Video Game", //10
                        ParentId = 2
                    },
                    new Tag
                    {
                        Name = "Writing", //11
                        ParentId = 2
                    },
                    new Tag
                    {
                        Name = "Music", //12
                        ParentId = 2
                    },
                    new Tag
                    {
                        Name = "Video", //13
                        ParentId = 2
                    },
                    //Person parents
                    new Tag
                    {
                        Name = "Artist", //14
                        ParentId = 3
                    },
                    //Art parents
                    new Tag
                    {
                        Name = "Applied", //15
                        ParentId = 4
                    },
                    new Tag
                    {
                        Name = "Performative", //16
                        ParentId = 4
                    },
                    new Tag
                    {
                        Name = "Visual", //17
                        ParentId = 4
                    },
                    new Tag
                    {
                        Name = "Decorative", //18
                        ParentId = 4
                    },
                    // Entity parents
                    new Tag
                    {
                        Name = "Plant", //19
                        ParentId = 7
                    },
                    new Tag
                    {
                        Name = "Animal", //20
                        ParentId = 7
                    },
                    new Tag
                    {
                        Name = "Monster", //21
                        ParentId = 7
                    },
                    //DESCRIPTIVE parent TAGS
                    new Tag
                    {
                        Name = "Fictional", //22
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Region", //23
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Time", //24
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Topic", //25
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Science", //26
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Sport", //27
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Genre", //28
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Fiction Genre", //29
                        ParentId = 28
                    },
                    new Tag
                    {
                        Name = "Japanese Genre", //30
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Music Genre", //31
                        ParentId = 28
                    },
                    new Tag
                    {
                        Name = "Videogame Genre", //32
                        ParentId = 28
                    },
                    //DEITY
                    new Tag
                    {
                        Name = "Deity",
                        ParentId = 7
                    },

                    //writing

                    new Tag
                    {
                        Name = "Anecdote", //14
                        ParentId = 11
                    },
                    new Tag
                    {
                        Name = "Article", //15
                        ParentId = 11
                    },
                    new Tag
                    {
                        Name = "Book", //16
                        ParentId = 11
                    },
                    new Tag
                    {
                        Name = "Essay", //17
                        ParentId = 11
                    },
                    new Tag
                    {
                        Name = "Legend", //18
                        ParentId = 11
                    },
                    new Tag
                    {
                        Name = "Paper", //19
                        ParentId = 11
                    },
                    new Tag
                    {
                        Name = "Poem", //20
                        ParentId = 11
                    },
                    new Tag
                    {
                        Name = "Quote", //21
                        ParentId = 11
                    },
                    new Tag
                    {
                        Name = "Tale", //22
                        ParentId = 11
                    },
                    //Music
                    new Tag
                    {
                        Name = "Album", 
                        ParentId = 12
                    },
                    new Tag
                    {
                        Name = "Single", 
                        ParentId = 12
                    },
                    new Tag
                    {
                        Name = "Mixtape", 
                        ParentId = 12
                    },
                    //Video
                    new Tag
                    {
                        Name = "Anime",
                        ParentId = 13
                    },
                    new Tag
                    {
                        Name = "Movie",
                        ParentId = 13
                    },
                    new Tag
                    {
                        Name = "Online Video",
                        ParentId = 13
                    }, 
                    new Tag
                    {
                        Name = "Series",
                        ParentId = 13
                    }, 
                    new Tag
                    {
                        Name = "Shortmovie",
                        ParentId = 13
                    },
                    //Person
                    new Tag
                    {
                        Name = "Activist",
                        ParentId = 3
                    },
                    new Tag
                    {
                        Name = "Athlete",
                        ParentId = 3
                    },
                    new Tag
                    {
                        Name = "Business Leader",
                        ParentId = 3
                    },
                    new Tag
                    {
                        Name = "Historian",
                        ParentId = 3
                    },
                    new Tag
                    {
                        Name = "Philosopher",
                        ParentId = 3
                    },
                    new Tag
                    {
                        Name = "Politician",
                        ParentId = 3
                    },
                    new Tag
                    {
                        Name = "Scientist",
                        ParentId = 3
                    },
                    //Artists
                    new Tag
                    {
                        Name = "Actor",
                        ParentId = 14
                    },
                    new Tag
                    {
                        Name = "Author",
                        ParentId = 14
                    },
                    new Tag
                    {
                        Name = "Comedian",
                        ParentId = 14
                    },
                    new Tag
                    {
                        Name = "Director",
                        ParentId = 14
                    },
                    new Tag
                    {
                        Name = "Digital Artist",
                        ParentId = 14
                    },
                    new Tag
                    {
                        Name = "Fine Artist",
                        ParentId = 14
                    },
                    new Tag
                    {
                        Name = "Musician",
                        ParentId = 14
                    },
                    new Tag
                    {
                        Name = "Painter",
                        ParentId = 14
                    },
                    new Tag
                    {
                        Name = "Photographer",
                        ParentId = 14
                    },
                    new Tag
                    {
                        Name = "Writer",
                        ParentId = 14
                    },
                    new Tag
                    {
                        Name = "Voice Actor",
                        ParentId = 14
                    },
                    //Arts
                    new Tag
                    {
                        Name = "Architecture",
                        ParentId = 15
                    },
                    new Tag
                    {
                        Name = "Fashion",
                        ParentId = 15
                    },
                    new Tag
                    {
                        Name = "Dance",
                        ParentId = 16
                    },
                    new Tag
                    {
                        Name = "Theatre Act",
                        ParentId = 16
                    },
                    new Tag
                    {
                        Name = "Drawing",
                        ParentId = 17
                    },
                    new Tag
                    {
                        Name = "Painting",
                        ParentId = 17
                    },
                    new Tag
                    {
                        Name = "Sculpture",
                        ParentId = 17
                    },
                    //Events
                    new Tag
                    {
                        Name = "Battle",
                        ParentId = 5
                    },
                    new Tag
                    {
                        Name = "Discovery",
                        ParentId = 5
                    },
                    new Tag
                    {
                        Name = "Holiday",
                        ParentId = 5
                    },
                    new Tag
                    {
                        Name = "Invention",
                        ParentId = 5
                    },
                    new Tag
                    {
                        Name = "Natural Phenomenom",
                        ParentId = 5
                    },
                    new Tag
                    {
                        Name = "Revolution",
                        ParentId = 5
                    },
                    new Tag
                    {
                        Name = "War",
                        ParentId = 5
                    },
                    //Place
                    new Tag
                    {
                        Name = "Ancient Civilization",
                        ParentId = 6
                    },
                    new Tag
                    {
                        Name = "Archaeological Site",
                        ParentId = 6
                    },
                    new Tag
                    {
                        Name = "Battlefield",
                        ParentId = 6
                    },
                    new Tag
                    {
                        Name = "Building",
                        ParentId = 6
                    },
                    new Tag
                    {
                        Name = "Landmark",
                        ParentId = 6
                    },
                    new Tag
                    {
                        Name = "Memorial",
                        ParentId = 6
                    },
                    new Tag
                    {
                        Name = "Monument",
                        ParentId = 6
                    },
                    new Tag
                    {
                        Name = "Palace or Castle",
                        ParentId = 6
                    },
                    new Tag
                    {
                        Name = "Religious Site",
                        ParentId = 6
                    },
                    new Tag
                    {
                        Name = "Ruins",
                        ParentId = 6
                    },
                    // Creatures
                    new Tag
                    {
                        Name = "Amphibian",
                        ParentId = 20
                    },
                    new Tag
                    {
                        Name = "Bird",
                        ParentId = 20
                    },
                    new Tag
                    {
                        Name = "Fish",
                        ParentId = 20
                    },
                    new Tag
                    {
                        Name = "Insect",
                        ParentId = 20
                    },
                    new Tag
                    {
                        Name = "Mammal",
                        ParentId = 20
                    },
                    new Tag
                    {
                        Name = "Reptile",
                        ParentId = 20
                    },
                    new Tag
                    {
                        Name = "Beast",
                        ParentId = 21
                    },
                    new Tag
                    {
                        Name = "Spirit",
                        ParentId = 21
                    },
                    // Gadgets
                    new Tag
                    {
                        Name = "For Creativity",
                        ParentId = 8
                    },
                    new Tag
                    {
                        Name = "For Entertainment",
                        ParentId = 8
                    },
                    new Tag
                    {
                        Name = "For Fitness",
                        ParentId = 8
                    },
                    new Tag
                    {
                        Name = "For Fun",
                        ParentId = 8
                    },
                    new Tag
                    {
                        Name = "For Home",
                        ParentId = 8
                    },
                    new Tag
                    {
                        Name = "For Learning",
                        ParentId = 8
                    },
                    new Tag
                    {
                        Name = "For Outdoor Activity",
                        ParentId = 8
                    },
                    new Tag
                    {
                        Name = "For Productivity",
                        ParentId = 8
                    },
                    new Tag
                    {
                        Name = "For Travel",
                        ParentId = 8
                    },
                    // Places
                    new Tag
                    {
                        Name = "Board",
                        ParentId = 9
                    },
                    new Tag
                    {
                        Name = "Card",
                        ParentId = 9
                    },
                    new Tag
                    {
                        Name = "Party",
                        ParentId = 9
                    },
                    new Tag
                    {
                        Name = "Puzzle",
                        ParentId = 9
                    },

                    //Descriptive children Tags
                    //Fictional
                    new Tag
                    {
                        Name = "Mythological",
                        ParentId = 22
                    },
                    //Regions
                    new Tag
                    {
                        Name = "Africa",
                        ParentId = 23
                    },
                    new Tag
                    {
                        Name = "Asia",
                        ParentId = 23
                    },
                    new Tag
                    {
                        Name = "Europe",
                        ParentId = 23
                    },
                    new Tag
                    {
                        Name = "North America",
                        ParentId = 23
                    },
                    new Tag
                    {
                        Name = "Middle East",
                        ParentId = 23
                    },
                    new Tag
                    {
                        Name = "Oceania",
                        ParentId = 23
                    },
                    new Tag
                    {
                        Name = "South America",
                        ParentId = 23
                    },
                    //Times
                    new Tag
                    {
                        Name = "Ancient",
                        ParentId = 24
                    },
                    new Tag
                    {
                        Name = "Medieval 5-15th",
                        ParentId = 24
                    },
                    new Tag
                    {
                        Name = "Renaissance 14-17th",
                        ParentId = 24
                    },
                    new Tag
                    {
                        Name = "Enlightenment 17-18th",
                        ParentId = 24
                    },
                    new Tag
                    {
                        Name = "Industrial Revolution 18-19th",
                        ParentId = 24
                    },
                    new Tag
                    {
                        Name = "Modern Era 19-20th",
                        ParentId = 24
                    },
                    new Tag
                    {
                        Name = "Contemporary Era 20-21th",
                        ParentId = 24
                    },
                    new Tag
                    {
                        Name = "21th Century",
                        ParentId = 24
                    },
                    //SAVE FOR CHILDREN HAS THING AS PARENT NOW
                    new Tag
                    {
                        Name = "Safe For Children",
                        ParentId = 1
                    },
                    new Tag
                    {
                        Name = "Humor",
                        ParentId = 1
                    },
                    //Topics
                    new Tag
                    {
                        Name = "About Children",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Art & Photography",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Biography",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Commentary",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Documentary",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Finance",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Guide",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Life Advice",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Memoir",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Parenting",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Past Civilization",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Philosophy",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Politics",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Relationships",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Religion",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Self Help",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Social Science",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "Technology",
                        ParentId = 25
                    },
                    new Tag
                    {
                        Name = "True Crime",
                        ParentId = 25
                    },
                    // Sciences
                    new Tag
                    {
                        Name = "Archaeology",
                        ParentId = 26
                    },
                    new Tag
                    {
                        Name = "Astrology :)",
                        ParentId = 26
                    },
                    new Tag
                    {
                        Name = "Astronomy",
                        ParentId = 26
                    },
                    new Tag
                    {
                        Name = "Biology",
                        ParentId = 26
                    },
                    new Tag
                    {
                        Name = "Chemistry",
                        ParentId = 26
                    },
                    new Tag
                    {
                        Name = "Economy",
                        ParentId = 26
                    },
                    new Tag
                    {
                        Name = "Neuroscience",
                        ParentId = 26
                    },
                    new Tag
                    {
                        Name = "Physics",
                        ParentId = 26
                    },
                    new Tag
                    {
                        Name = "Psychology",
                        ParentId = 26
                    },
                    new Tag
                    {
                        Name = "Sociology",
                        ParentId = 26
                    },
                    new Tag
                    {
                        Name = "Computer Science",
                        ParentId = 26
                    },
                    //Sports
                    new Tag
                    {
                        Name = "Auto Racing",
                        ParentId = 27
                    },
                    new Tag
                    {
                        Name = "Baseball",
                        ParentId = 27
                    },
                    new Tag
                    {
                        Name = "Basketball",
                        ParentId = 27
                    },
                    new Tag
                    {
                        Name = "Cricket",
                        ParentId = 27
                    },
                    new Tag
                    {
                        Name = "Football",
                        ParentId = 27
                    },
                    new Tag
                    {
                        Name = "Golf",
                        ParentId = 27
                    },
                    new Tag
                    {
                        Name = "Hockey",
                        ParentId = 27
                    },
                    new Tag
                    {
                        Name = "Rugby",
                        ParentId = 27
                    },
                    new Tag
                    {
                        Name = "Tennis",
                        ParentId = 27
                    },
                    new Tag
                    {
                        Name = "Volleyball",
                        ParentId = 27
                    },
                    new Tag
                    {
                        Name = "Other Sports",
                        ParentId = 27
                    },
                    //Genres
                    new Tag
                    {
                        Name = "Indie",
                        ParentId = 28
                    },
                    //Fiction Genre
                    new Tag
                    {
                        Name = "Action",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Adventure",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Crime",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Drama",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Dystopian",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Fantasy",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Historical",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Horror",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Magical Realism",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Mystery",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Noir",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Romance",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Satire",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Science Fiction",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Swashbuckle",
                        ParentId = 29
                    },
                    new Tag
                    {
                        Name = "Thriller",
                        ParentId = 29
                    },
                    //Japanese Genres
                    new Tag
                    {
                        Name = "Shonen",
                        ParentId = 30
                    },
                    new Tag
                    {
                        Name = "Seinen",
                        ParentId = 30
                    },
                    new Tag
                    {
                        Name = "Shoji",
                        ParentId = 30
                    },
                    //Music Genres
                    new Tag
                    {
                        Name = "Classical",
                        ParentId = 31
                    },
                    new Tag
                    {
                        Name = "Electronic",
                        ParentId = 31
                    },
                    new Tag
                    {
                        Name = "Hip-Hop",
                        ParentId = 31
                    },
                    new Tag
                    {
                        Name = "Jazz",
                        ParentId = 31
                    },
                    new Tag
                    {
                        Name = "Metal",
                        ParentId = 31
                    },
                    new Tag
                    {
                        Name = "Pop",
                        ParentId = 31
                    },
                    new Tag
                    {
                        Name = "Reggae",
                        ParentId = 31
                    },
                    new Tag
                    {
                        Name = "R&B",
                        ParentId = 31
                    },
                    new Tag
                    {
                        Name = "Rock",
                        ParentId = 31
                    },
                    new Tag
                    {
                        Name = "Soundtrack",
                        ParentId = 31
                    },
                    //Videogame Genres
                    new Tag
                    {
                        Name = "MMORPG",
                        ParentId = 32
                    },
                    new Tag
                    {
                        Name = "Multiplayer",
                        ParentId = 32
                    },
                    new Tag
                    {
                        Name = "RPG",
                        ParentId = 32
                    },
                    new Tag
                    {
                        Name = "Sandbox",
                        ParentId = 32
                    },
                    new Tag
                    {
                        Name = "Simulation",
                        ParentId = 32
                    },
                    new Tag
                    {
                        Name = "Shooter",
                        ParentId = 32
                    }

                ); ;
                count++;
            }

            
            if(count > 0) context.SaveChanges();
        }
    }
}