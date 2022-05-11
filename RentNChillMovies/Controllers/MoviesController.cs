using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentNChillMovies.Data;
using RentNChillMovies.Interfaces;
using RentNChillMovies.Models;
using RentNChillMovies.ViewModels;



namespace RentNChillMovies.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment host;
        private readonly IMoviesRepository _movies;
        public MoviesController(ApplicationDbContext _context, IWebHostEnvironment host, IMoviesRepository _movies, UserManager<User> _userManager)
        {
            this._context = _context;
            this.host = host;
            this._movies = _movies;
            this._userManager = _userManager;
        }
        
        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in _context.Genres orderby m.GenreId select m.GenreName;

            var movies = from m in _context.Movies select m;
            var userLikes = new List<UserLike>();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.MovieTitle.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre.GenreName == movieGenre);
            }
            userLikes = _context.UserLikes.ToList();

            var movieGereVm = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync(),
                UserLikes =userLikes
            };
            return View(movieGereVm);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int id)
        {
            Movie movie = _movies.GetMovies(id);
            var movieActor = _context.MovieActors.Include(a => a.Actor).Where(m => m.MovieId == id);
            var movieProducer = _context.MovieProducers.Include(p => p.Producer).Where(m => m.MovieId == id);
            var userLikes = new List<UserLike>();
            var totalVotes = _context.UserLikes.Where(b=> b.MovieId == id).Count();
            var totalLikes = _context.UserLikes.Where(b => b.MovieId == id).Where(c => c.IsLike == true).Count();
            var totalDislikes = _context.UserLikes.Where(b => b.MovieId == id).Where(c => c.IsDislike == true).Count();
            var percentageVotes = 0;
            var users =  _context.Users.ToList();   
           

            if (totalVotes == 0)
            {
                percentageVotes = 0;
            }
            else
            {
                 percentageVotes = (totalLikes / totalVotes) * 100;
            }
            

            var userMovies = _context.UserMovies.Include(m => m.Movie).Where(u => u.MovieId == id);
            var rev = _context.MovieReviews.Include(r => r.Movie).Where(re => re.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }
            userLikes = _context.UserLikes.ToList();
            return View(new MovieViewModel
            {
                Movie = movie,
                movieActors = movieActor,
                movieProducers = movieProducer,
                UserLikes = userLikes,
                TotalVotes = totalVotes,
                TotalLikes = totalLikes,
                TotalDislikes= totalDislikes,
                PercentageVotes = percentageVotes,
                movieReviews = rev,
                Users = users
                
                
                
            });
        }

        // GET: Movies/Create

        public IActionResult Create()

        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName");
            

            var actors =  _context.Actors.ToList();
            var producers = _context.Producers.ToList();
            return View(new MovieViewModel
            {
                Actors =  actors,
                Producers = producers
            });
               
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create (MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                await _movies.PostNewMovie(movieViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(movieViewModel);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(int id)
        {
            Movie movie = _movies.GetMovies(id);

            Movie movies = new Movie()
            {
                MovieId = movie.MovieId,
                MovieTitle=movie.MovieTitle,
                MovieDescription=movie.MovieDescription,
                //MovieReviews=movie.MovieReviews,
                MovieThumbnail=movie.MovieThumbnail,
                MovieTrailer=movie.MovieTrailer,
                AudienceCode=movie.AudienceCode,
                Genre=movie.Genre,
                GenreId=movie.GenreId,
                ImdbURL=movie.ImdbURL,
                IsAvailable=movie.IsAvailable,
                Language=movie.Language,
                Price=movie.Price,
                ReleaseDate=movie.ReleaseDate,
                RottenTomatoesURL=movie.RottenTomatoesURL
            };
        
            var actors = _context.Actors.ToList();
            var producers = _context.Producers.ToList();
        
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", movie.GenreId);

            return View(new MovieViewModel
            {
                Movie = movies,
                Producers=producers,
                Actors=actors
            });
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                await _movies.UpdateMovies(movieViewModel);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreName", movieViewModel.Movie.GenreId);
            return View(movieViewModel);
        }

        // GET: Movies/Delete/5
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
    
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            var imagePath = Path.Combine(host.WebRootPath, movie.MovieThumbnail);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }
    }
}
