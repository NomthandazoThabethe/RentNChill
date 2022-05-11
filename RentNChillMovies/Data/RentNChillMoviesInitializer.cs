using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using RentNChillMovies.Models;
using RentNChillMovies.Enums;

namespace RentNChillMovies.Data
{
    public static class RentNChillMoviesInitializer
    {
        public static async Task SeedRoles(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
              await roleManager.CreateAsync(new IdentityRole(Roles.Administrator.ToString()));
              await roleManager.CreateAsync(new IdentityRole(Roles.Customer.ToString()));
            
        }

        public static async Task SeedAdmin(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new User
            {
                UserName = "Admin",
                Email = "admin@gmail.com",
                FirstName = "Minato",
                LastName = "Namikazi",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PhoneNumber = "082585788"
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Asdfghjkl;'456");
                    await userManager.AddToRoleAsync(defaultUser,Roles.Administrator.ToString());
                }

            }
        }
            public static void Initialize(ApplicationDbContext _context)
        {
            if (_context.Movies.Any())
            {
                return;
            }

            var genres = new List<Genre>
            {
                new Genre
                {
                    GenreName = "Sci-fi"
                },
                new Genre
                {
                    GenreName = "Action"
                },
                new Genre
                {
                    GenreName = "Horror"
                },
                new Genre
                {
                    GenreName = "Comedy"
                },
                new Genre
                {
                    GenreName = "Animation"
                }
            };
            genres.ForEach(g => _context.Genres.Add(g));
            _context.SaveChanges();

            var actors = new List<Actor>
            {
                new Actor
                {
                    ActorImage = "/images/actorThumbails/Michael J. Fox.jpg",
                    ActorName = "Michael J.",
                    ActorLastName = "Fox",
                    ActorBio = @"Michael J. Fox was born Michael Andrew Fox on June 9, 1961 in Edmonton, Alberta, Canada, to Phyllis Fox (née Piper), a payroll clerk, and William Fox. His parents moved their 10-year-old son, his three sisters, Kelli Fox, Karen, and Jacki, and his brother Steven, to Vancouver, British Columbia, after his father, a sergeant in the Canadian Army Signal Corps, retired. During these years Michael developed his desire to act. At 15 he successfully auditioned for the role of a 10-year-old in a series called Leo and Me (1978). Gaining attention as a bright new star in Canadian television and movies, Michael realized his love for acting when he appeared on stage in The Shadow Box. At 18 he moved to Los Angeles and was offered a few television-series roles, but soon they stopped coming and he was surviving on boxes of macaroni and cheese. Then his agent called to tell him that he got the part of Alex P. Keaton on the situation comedy Family Ties (1982). He starred in the feature films Teen Wolf (1985), High School U.S.A. (1983), Poison Ivy (1985) and Back to the Future (1985)."
                },
                new Actor
                {
                    ActorImage = "/images/actorThumbails/Scarlett Johansson.jpg",
                    ActorName = "Scarlett",
                    ActorLastName = "Johansson",
                    ActorBio = "Scarlett Ingrid Johansson was born on November 22, 1984 in Manhattan, New York City, New York. Her mother, Melanie Sloan is from a Jewish family from the Bronx and her father, Karsten Johansson is a Danish-born architect from Copenhagen. She has a sister, Vanessa Johansson, who is also an actress, a brother, Adrian, a twin brother, Hunter Johansson, born three minutes after her, and a paternal half-brother, Christian. Her grandfather was writer Ejner Johansson. Johansson began acting during childhood, after her mother started taking her to auditions. She made her professional acting debut at the age of eight in the off-Broadway production of \"Sophistry\" with Ethan Hawke, at New York's Playwrights Horizons. She would audition for commercials but took rejection so hard her mother began limiting her to film tryouts. She made her film debut at the age of nine, as John Ritter's character's daughter in the fantasy comedy North (1994). Following minor roles in Just Cause (1995), as the daughter of Sean Connery and Kate Capshaw's character, and If Lucy Fell (1996), she played the role of Amanda in Manny & Lo (1996). Her performance in Manny & Lo garnered a nomination for the Independent Spirit Award for Best Lead Female, and positive reviews, one noting, \"[the film] grows on you, largely because of the charm of ... Scarlett Johansson\", while San Francisco Chronicle critic Mick LaSalle commentated on her \"peaceful aura\", and wrote, \"If she can get through puberty with that aura undisturbed, she could become an important actress.\" After appearing in minor roles in Fall (1997) and Home Alone 3 (1997), Johansson garnered widely spread attention for her performance in The Horse Whisperer (1998), directed by Robert Redford, where she played Grace MacLean, a teenager traumatized by a riding accident. She received a nomination for the Chicago Film Critics Association Award for Most Promising Actress for the film. In 1999, she appeared in My Brother the Pig (1999) and in the music video for Mandy Moore's single, \"Candy\". Although the film was not a box office success, she received praise for her breakout role in Ghost World (2001), credited with \"sensitivity and talent [that] belie her age\". She was also featured in the Coen Brothers' dark drama The Man Who Wasn't There (2001), opposite Billy Bob Thornton and Frances McDormand. She appeared in the horror comedy Eight Legged Freaks (2002) with David Arquette and Kari Wuhrer. In 2003, she was nominated for two Golden Globe Awards, one for drama (Girl with a Pearl Earring (2003)) and one for comedy (Lost in Translation (2003)), her breakout role, starring opposite Bill Murray, and receiving rave reviews and a Best Actress Award at the Venice Film Festival. Her film roles include the critically acclaimed Weitz brothers' film In Good Company (2004), as well as starring opposite John Travolta in A Love Song for Bobby Long (2004), which garnered her a third Golden Globe Award nomination. She dropped out of Mission: Impossible III (2006) due to scheduling conflicts. Her next film role was in The Island (2005) alongside Ewan McGregor which earned weak reviews from U.S. critics. After this, she appeared in Woody Allen's Match Point (2005) and was nominated again for a Golden Globe Award. In May 2008, she released her album \"Anywhere I Lay My Head\", a collection of Tom Waits covers featuring one original song. Also that year, she starred in Frank Miller's The Spirit (2008), the Woody Allen film Vicky Cristina Barcelona (2008), and played Mary Boleyn opposite Natalie Portman in The Other Boleyn Girl (2008). Since then, she has appeared as part of an ensemble cast in the romantic comedy He's Just Not That Into You (2009), the action superhero film Iron Man 2 (2010), the comedy-drama We Bought a Zoo (2011) and starred as the original scream queen, Janet Leigh, in Hitchcock (2012). She then played her character, Black Widow, in the blockbuster action films The Avengers (2012), Captain America: The Winter Soldier (2014), Avengers: Age of Ultron (2015), Captain America: Civil War (2016), Avengers: Infinity War (2018), Avengers: Endgame (2019) and Black Widow (2021), and also headlined the sci-fi action thriller Lucy (2014), a box office success. With more than a decade of work already under her belt, Scarlett has proven to be one of Hollywood's most talented young actresses. Her other starring roles are in the sci-fi action thriller Ghost in the Shell (2017) and the dark comedy Rough Night (2017). Scarlett and Canadian actor Ryan Reynolds were engaged in May 2008 and married in September of that year. In 2010, the couple announced their separation, and subsequently divorced a year later. In 2013, she became engaged to French journalist Romain Dauriac, the couple married a year later. In January 2017, the couple announced their separation, and subsequently divorced in March of that year. They have a daughter, Rose Dorothy Dauriac (born 2014)."
                },
                new Actor
                {
                    ActorImage = "/images/actorThumbails/Emily Blunt.jpg",
                    ActorName = "Emily",
                    ActorLastName = "Blunt",
                    ActorBio = "Emily Olivia Leah Blunt is a British actress known for her roles in The Devil Wears Prada (2006), The Young Victoria (2009), Edge of Tomorrow (2014), and The Girl on the Train (2016), among many others. Blunt was born on February 23, 1983, in Roehampton, South West London, England, the second of four children in the family of Joanna Mackie, a former actress and teacher, and Oliver Simon Peter Blunt, a barrister. Her grandfather was Major General Peter Blunt, and her uncle is MP Crispin Blunt. Emily received a rigorous education at Ibstock Place School, a co-ed private school at Roehampton. However, young Emily Blunt had a stammer, since she was a kid of 8. Her mother took her to relaxation classes, which did not do anything. She reached a turning point at 12, when a teacher cleverly asked her to play a character with a different voice and said, \"I really believe in you\". Blunt ended up using a northern accent, and it did the trick, her stammer disappeared. From 1999 - 2001, Blunt went to Hurtwood House, the top co-ed boarding school where she would excel at sport, cello and singing. She also had two years of drama studies at Hurtwood's theatre course. In August 2000, she was chosen to perform at the Edinburgh Festival. She was signed up by an agent, Kenneth Mcreddie, who led her to the West End and the BBC, scoring her roles in several period dramas on stage as well as on TV productions, such as Foyle's War (2002), Henry VIII (2003) and Empire (2005). In 2001, she appeared as \"Gwen Cavendish\" opposite Dame Judi Dench in Sir Peter Hall's production of \"The Royal Family\" at Haymarket Theatre. For that role, she won the Evening Standard Award for Best Newcomer. In 2002, she played \"Juliet\" in \"Romeo and Juliet\" at the prestigious Chichester Festival. Blunt's career ascended to international fame after she starred as \"Isolda\" opposite Alex Kingston in Warrior Queen (2003). A year later, she won critical acclaim for her breakout performance as \"Tamsin\", a well-educated, cynical and deceptive 16-year-old beauty in My Summer of Love (2004), a story of two lonely girls from the opposite ends of the social heap. Emily Blunt and her co-star, Natalie Press, shared an Evening Standard British Film award for Most Promising Newcomer. In 2005, she spent a few months in Australia filming Irresistible (2006) with Susan Sarandon and Sam Neill. Blunt gave an impressive performance as \"Mara\", a cunning young destroyer who acts crazy and surreptitiously provokes paranoia in others. She also continued her work on British television, starring as \"Natasha\" in Stephen Poliakoff's Gideon's Daughter (2005), opposite Bill Nighy, a role that won her a 2007 Golden Globe Award for Best Performance by an Actress in a Supporting Role. She continued the line of playing manipulative characters as \"Emily\", a caustic put-upon assistant to Meryl Streep's lead in The Devil Wears Prada (2006). Blunt's performance with a neurotic twist added a dimension of sarcasm to the comedy, and gained her much attention as well as new jobs: in two dramas opposite Tom Hanks, then in the title role in the period drama, The Young Victoria (2009). Her most recent works include appearances as antiques dealer \"Gwen Conliffe\" in The Wolfman (2010) and as the ballerina in The Adjustment Bureau (2011). Emily is a highly versatile actress and a multifaceted person. Her talents include singing and playing cello; she is also skilled at horseback riding. On August 28, 2009, Blunt and Krasinski announced their engagement. The couple married on July 10, 2010, at the estate of their friend, George Clooney, on Lake Como in Italy. Blunt and Krasinski live in the Los Angeles area, California, and have two children."
                },
                new Actor
                {
                    ActorImage = "/images/actorThumbails/Ryan Reynolds.jpg",
                    ActorName = "Ryan",
                    ActorLastName = "Reynolds ",
                    ActorBio = "Ryan Rodney Reynolds was born on October 23, 1976 in Vancouver, British Columbia, Canada, the youngest of four children. His father, James Chester Reynolds, was a food wholesaler, and his mother, Tamara Lee \"Tammy\" (Stewart), worked as a retail-store saleswoman. He has Irish and Scottish ancestry. Between 1991-93, Ryan appeared in Fifteen (1990), a Nickleodeon series taped in Florida with many other Canadian actors. After the series ended, he returned to Vancouver where he played in a series of forgettable television movies. He did small roles in Glenn Close's Serving in Silence: The Margarethe Cammermeyer Story (1995) and CBS's update of In Cold Blood (1996). However, his run of luck had led him to decide to quit acting. One night, he ran into fellow Vancouver actor and native Chris William Martin. Martin found Ryan rather despondent and told him to pack everything: they were going to head to Los Angeles, California. The two stayed in a cheap Los Angeles motel. On the first night of their stay, Reynolds' jeep was rolled downhill and stripped. For the next four months, Ryan drove it without doors. In 1997, he landed the role of Berg in Two Guys, a Girl and a Pizza Place (1998). Initially, the show was reviled by critics and seemed desperate for any type of ratings success. However, it was renewed for a second season but with a provision for a makeover by former Roseanne (1988) writer Kevin Abbott. The show became a minor success and has led to additional film roles for Ryan, most notably in the last-ever MGM film, a remake of The Amityville Horror (2005). Ryan was engaged to Canadian singer-songwriter Alanis Morissette, another Nickelodeon veteran, between 2004-2006. He has been married to Blake Lively since September 9, 2012. They have three daughters. He was previously married to Scarlett Johansson."
                },
                new Actor
                {
                    ActorImage = "/images/actorThumbails/Kelly Marie Tran.jpg",
                    ActorName = "Kelly Marie",
                    ActorLastName = "Tran ",
                    ActorBio = "Kelly Marie Tran was born on January 17, 1989 in San Diego, California, USA. She is an actress and producer, known for Raya and the Last Dragon (2021), Star Wars: Episode VIII - The Last Jedi (2017) and Star Wars: Episode IX - The Rise of Skywalker (2019)."
                }
            };
            actors.ForEach(a => _context.Actors.Add(a));
            _context.SaveChanges();

            var producers = new List<Producer>
            {
                new Producer
                {
                    ProducerImage = "/images/producerThumbnails/Bob Gale.jpg",
                    ProducerFirstName = "Bob",
                    ProducerLastName = "Gale",
                    ProducerBio = "Bob Gale is an Oscar-nominated screenwriter-producer-director, best known as co-creator, co-writer and co-producer of Back to the Future (1985) and its sequels. Gale was born and raised in St. Louis, Missouri, and graduated Phi Beta Kappa with a B.A. in Cinema from the University of Southern California in 1973. He has written over 30 screenplays; his other film credits include 1941 (1979), I Wanna Hold Your Hand (1978), Used Cars (1980), Trespass (1992) and Interstate 60: Episodes of the Road (2002), the latter which he directed. In addition to writing movies and occasionally television, Gale has written comic books including Spider-Man, Batman and the IDW Back to the Future title, thus proving to his father that he did not waste hours and hours reading comics in his youth. He has also served as an expert witness in over 25 plagiarism cases, even though this has occasionally required him to wear a suit and tie (oh, the horror!). When he's not in production, writing, shooting off his mouth or wasting time on the internet, he actually does take out the trash even when his wife doesn't ask. Well, sometimes he does..."
                },
                new Producer
                {
                    ProducerImage = "/images/producerThumbnails/Jac Schaeffer.jpg",
                    ProducerFirstName = "Jac",
                    ProducerLastName = "Schaeffer",
                    ProducerBio = "Jac Schaeffer is a writer and producer, known for Timer (2009), Black Widow (2021) and WandaVision (2021)."
                },
                new Producer
                {
                    ProducerImage = "/images/producerThumbnails/John Krasinski.jpg",
                    ProducerFirstName = "John",
                    ProducerLastName = "Krasinski",
                    ProducerBio = "Tall, handsome American film and television star John Krasinski is known for his role as sardonic nice guy Jim Halpert on NBC's popular TV series, The Office (2005), for which he won a 2007 and 2008 Screen Actors Guild Award for outstanding performance by an ensemble in a comedy series. Born John Burke Krasinski on October 20, 1979, in Newton, Massachusetts, USA, he is the youngest of three brothers. His mother, Mary Claire (Doyle), is a nurse, and his father, Ronald Krasinski, is an internist. His father is of Polish descent and his mother is of Irish ancestry. His first stage experience was starring in a satirical high school play, written and cast by his classmate B.J. Novak. Also good at sports, he played on the same Little League baseball team as Novak, later a writer and co-star on The Office (2005). After graduating from Newton South High School in 1997, Krasinski planned to be an English major and deferred his first semester of college to teach English in Costa Rica. He attended Brown University, graduating in 2001, as a playwright with honors, then studied at the Eugene O'Neill National Theatre Institute in Waterford, Connecticut. During the summer of 2000, he worked as a script intern on Late Night with Conan O'Brien (1993). Krasinski made his big screen debut in 2002, then played several small roles like \"Ben\" in Kinsey (2004), and \"Bob Flynn\" in Duane Hopwood (2005). He appeared as \"Corporal Harrigan\" in Jarhead (2005), by director Sam Mendes, then played a supporting role as \"Ben\" in The Holiday (2006), a romantic comedy by director Nancy Meyers. He is billed as the voice of \"Lancelot\" in Shrek the Third (2007). Krasinski co-starred opposite Robin Williams and Mandy Moore in the romantic comedy License to Wed (2007), as well as with George Clooney and Renée Zellweger in the football screwball comedy, Leatherheads (2008). He is also director and writer of Brief Interviews with Hideous Men (2009), a big screen adaptation of the eponymous collection of short stories by David Foster Wallace. He followed that film up with The Hollars (2016), a family drama, and A Quiet Place (2018), a well-received horror film that had one of the biggest opening weekends for the genre. Krasinski is married to English actress Emily Blunt, with whom he has two children. He claims Los Angeles as his home but travels to New York City and his hometown of Newton, MA, frequently."
                },
                new Producer
                {
                    ProducerImage = "/images/producerThumbnails/Shawn Levy.jpg",
                    ProducerFirstName = "Shawn",
                    ProducerLastName = "Levy",
                    ProducerBio = "Shawn Levy was born on July 23, 1968 in Montreal, Quebec, Canada. He is a producer and director, known for Stranger Things (2016), Real Steel (2011), and the Night at the Museum franchise. He is the founder and principal of 21 Laps Entertainment. He is married to Serena Levy and they have four daughters."
                },
                new Producer
                {
                    ProducerImage = "/images/producerThumbnails/Qui Nguyen.jpg",
                    ProducerFirstName = "Qui",
                    ProducerLastName = "Nguyen",
                    ProducerBio = "Qui Nguyen is an award-winning playwright, TV writer, and screenwriter. For TV, he's written for SYFY's Incorporated (2016) and PBS's Peg+Cat (2013) where he won a Daytime Emmy for Outstanding Writing in a Preschool Animated Program. As a playwright, he's the Co-Founder of the OBIE Award-winning Vampire Cowboys Theatre Company of NYC, who are often credited as the pioneers of \"geek theatre.\" His plays have been produced at Manhattan Theatre Club, Steppenwolf, Oregon Shakespeare Festival, South Coast Rep, Mixed Blood, Seattle Rep, and many others."
                }
            };
            producers.ForEach(p => _context.Producers.Add(p));
            _context.SaveChanges();

            var movies = new List<Movie>
            {
                new Movie
                {
                    GenreId = 1,
                    MovieTitle = "Back to the Future",
                    ReleaseDate = DateTime.Parse("1985-07-03"),
                    Price = 5.00,
                    MovieDescription = "Marty travels back in time using an eccentric scientist's time machine. However, he must make his high-school-aged parents fall in love in order to return to the present.",
                    ImdbURL = "https://www.imdb.com/title/tt0088763/?ref_=fn_al_tt_1",
                    RottenTomatoesURL = "https://www.rottentomatoes.com/m/back_to_the_future",
                    AudienceCode = "PG",
                    Language ="English",
                    MovieThumbnail = "/images/movieThumbnails/Back to the Future.jpg",
                    MovieTrailer = "https://www.youtube.com/embed/qvsgGtivCgs",
                    IsAvailable = true
                },
                new Movie
                {
                    GenreId = 2,
                    MovieTitle = "Black Widow",
                    ReleaseDate = DateTime.Parse("2021-07-07"),
                    Price = 15.00,
                    MovieDescription = "Natasha Romanoff, aka Black Widow, confronts the darker parts of her ledger when a dangerous conspiracy with ties to her past arises. Pursued by a force that will stop at nothing to bring her down, Natasha must deal with her history as a spy, and the broken relationships left in her wake long before she became an Avenger.",
                    ImdbURL = "https://www.imdb.com/title/tt3480822/?ref_=fn_al_tt_1",
                    RottenTomatoesURL = "https://www.rottentomatoes.com/m/black_widow_2021",
                    AudienceCode = "PG-13",
                    Language ="English",
                    MovieThumbnail = "/images/movieThumbnails/Black Widow.jpg",
                    MovieTrailer = "https://www.youtube.com/embed/ybji16u608U",
                    IsAvailable = true
                },
                new Movie
                {
                    GenreId = 3,
                    MovieTitle = "A Quiet Place Part II",
                    ReleaseDate = DateTime.Parse("2021-05-03"),
                    Price = 15.00,
                    MovieDescription = "Following the events at home, the Abbott family now face the terrors of the outside world. Forced to venture into the unknown, they realize the creatures that hunt by sound are not the only threats lurking beyond the sand path.",
                    ImdbURL = "https://www.imdb.com/title/tt14741256/?ref_=fn_al_tt_2",
                    RottenTomatoesURL = "https://www.rottentomatoes.com/m/a_quiet_place_part_ii",
                    AudienceCode = "PG-13",
                    Language ="English",
                    MovieThumbnail = "/images/movieThumbnails/A Quiet Place Part II.jpg",
                    MovieTrailer = "https://www.youtube.com/embed/BpdDN9d9Jio",
                    IsAvailable = true
                },
                new Movie
                {
                    GenreId = 4,
                    MovieTitle = "Free Guy",
                    ReleaseDate = DateTime.Parse("2021-08-13"),
                    Price = 15.00,
                    MovieDescription = "A bank teller discovers that he's actually an NPC inside a brutal, open world video game.",
                    ImdbURL = "https://www.imdb.com/title/tt6264654/?ref_=fn_al_tt_1",
                    RottenTomatoesURL = "https://www.rottentomatoes.com/m/free_guy",
                    AudienceCode = "PG-13",
                    Language ="English",
                    MovieThumbnail = "/images/movieThumbnails/Free Guy.jpg",
                    MovieTrailer = "https://www.youtube.com/embed/JORN2hkXLyM",
                    IsAvailable = false
                },
                new Movie
                {
                    GenreId = 5,
                    MovieTitle = "Raya and the Last Dragon",
                    ReleaseDate = DateTime.Parse("2021-03-05"),
                    Price = 15.00,
                    MovieDescription = "In a realm known as Kumandra, a re-imagined Earth inhabited by an ancient civilization, a warrior named Raya is determined to find the last dragon.",
                    ImdbURL = "https://www.imdb.com/title/tt5109280/?ref_=nv_sr_srsg_0",
                    RottenTomatoesURL = "https://www.rottentomatoes.com/m/raya_and_the_last_dragon",
                    AudienceCode = "PG",
                    Language ="English",
                    MovieThumbnail = "/images/movieThumbnails/Raya and the Last Dragon.jpg",
                    MovieTrailer = "https://www.youtube.com/embed/1VIZ89FEjYI",
                    IsAvailable = true
                }

            };
            movies.ForEach(m => _context.Movies.Add(m));
            _context.SaveChanges();

            var movieActors = new List<MovieActor>
            {
                new MovieActor
                {
                    ActorId = 1,
                    MovieId = 1
                },
                new MovieActor
                {
                    ActorId = 2,
                    MovieId = 2
                },
                new MovieActor
                {
                    ActorId = 3,
                    MovieId = 3
                },
                new MovieActor
                {
                    ActorId = 4,
                    MovieId = 4
                },
                new MovieActor
                {
                    ActorId = 5,
                    MovieId = 5
                }
            };
            movieActors.ForEach(ma => _context.MovieActors.Add(ma));
            _context.SaveChanges();

            var movieProducers = new List<MovieProducer>
            {
                new MovieProducer
                {
                    ProducerId = 1,
                    MovieId = 1
                },
                new MovieProducer
                {
                    ProducerId = 2,
                    MovieId = 2
                },
                new MovieProducer
                {
                    ProducerId = 3,
                    MovieId = 3
                },
                new MovieProducer
                {
                    ProducerId = 4,
                    MovieId = 4
                },
                new MovieProducer
                {
                    ProducerId = 5,
                    MovieId = 5
                },
            };
            movieProducers.ForEach(mp => _context.MovieProducers.Add(mp));
            _context.SaveChanges();

            var rentals = new List<Rental>
            {
                new Rental
                {
                   // UserId = "ca1865f6-23f0-4ada-90d4-e7cf38cee77a",
                    MovieId = 1,
                    RentalDateStart = DateTime.Parse("2021-01-01"),
                    RentalDateEnd = DateTime.Parse("2021-01-10"),
                    RentalAmount = 15.00
                },
                new Rental
                {
                    
                   // UserId = "ba1865f6-23f0-4ada-90d4-e7cf38cee77a",
                    MovieId = 2,
                    RentalDateStart = DateTime.Parse("2021-01-20"),
                    RentalDateEnd = DateTime.Parse("2021-01-30"),
                    RentalAmount = 8.00
                },
                new Rental
                {
                   // UserId = "da1865f6-23f0-4ada-90d4-e7cf38cee77a",
                    MovieId = 3,
                    RentalDateStart = DateTime.Parse("2021-05-05"),
                    RentalDateEnd = DateTime.Parse("2021-05-15"),
                    RentalAmount = 15.00
                },
                new Rental
                {
                   // UserId = "ea1865f6-23f0-4ada-90d4-e7cf38cee77a",
                    MovieId = 2,
                    RentalDateStart = DateTime.Parse("2021-07-11"),
                    RentalDateEnd = DateTime.Parse("2021-07-21"),
                    RentalAmount = 15.00
                },
                new Rental
                {
                   // UserId = "fa1865f6-23f0-4ada-90d4-e7cf38cee77a",
                    MovieId = 5,
                    RentalDateStart = DateTime.Parse("2021-07-01"),
                    RentalDateEnd = DateTime.Parse("2021-07-10"),
                    RentalAmount = 15.00
                }
            };
            rentals.ForEach(r => _context.Rentals.Add(r));
            _context.SaveChanges();

            var accounts = new List<Account>
            {
                new Account
                {
                   // UserId = "ca1865f6-23f0-4ada-90d4-e7cf38cee77a",
                    BankName = "Capitec",
                    CardNumber = "5262051234567890",
                    AccountHolderName = "Mulanga Siphuma",
                    CVV=267,
                    ExpireDate=12/18
                    
                },
                new Account
                {
                   // UserId = "ba1865f6-23f0-4ada-90d4-e7cf38cee77a",
                    BankName = "Absa",
                    CardNumber = "5262052134567890",
                    AccountHolderName = "Senzo Mkupa",
                    CVV=269,
                    ExpireDate=11/08
                },
                new Account
                {
                   // UserId = "da1865f6-23f0-4ada-90d4-e7cf38cee77a",
                    BankName = "Fnb",
                    CardNumber = "5262051235467980",
                    AccountHolderName = "Elton Daries",
                    CVV=205,
                    ExpireDate=02/14

                },
                new Account
                {
                   // UserId = "ea1865f6-23f0-4ada-90d4-e7cf38cee77a",
                    BankName = "Tymebank",
                    CardNumber = "5262051234576890",
                    AccountHolderName = "Nomthandazo Thabethe",
                    CVV=267,
                    ExpireDate=12/18
                },
                new Account
                {
                  //  UserId ="fa1865f6-23f0-4ada-90d4-e7cf38cee77a",
                    BankName = "Nedbank",
                    CardNumber = "5262051235467890",
                    AccountHolderName = "Emmanuel Shekinah",
                    CVV=213,
                    ExpireDate=06/26
                }
            };
            accounts.ForEach(a => _context.Accounts.Add(a));
            _context.SaveChanges();

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    AccountId = 1,
                    RentId = 1,
                    TransactionDate = DateTime.Parse("2021-01-01"),
                    TransactionAmount = 15.00,
                    IsCompleted = true
                },
                new Transaction
                {
                    AccountId = 2,
                    RentId = 2,
                    TransactionDate = DateTime.Parse("2021-01-20"),
                    TransactionAmount = 15.00,
                    IsCompleted = false
                },
                new Transaction
                {
                    AccountId = 3,
                    RentId = 3,
                    TransactionDate = DateTime.Parse("2021-05-05"),
                    TransactionAmount = 15.00,
                    IsCompleted = true
                },
                new Transaction
                {
                    AccountId = 4,
                    RentId = 4,
                    TransactionDate = DateTime.Parse("2021-07-11"),
                    TransactionAmount = 15.00,
                    IsCompleted = true
                },
                new Transaction
                {
                    AccountId = 5,
                    RentId = 5,
                    TransactionDate = DateTime.Parse("2021-07-01"),
                    TransactionAmount = 15.00,
                    IsCompleted = true
                }
            };
            transactions.ForEach(t => _context.Transactions.Add(t));
            _context.SaveChanges();

            var userMovies = new List<UserMovie>
            {
                new UserMovie
                {
                    MovieId=1,
                    IsTrasactionComplete =false,
                    RentalEndDate = DateTime.Parse("2021-01-10")
                },

            };
            userMovies.ForEach(t => _context.UserMovies.Add(t));
            _context.SaveChanges();
        }
    }
}
