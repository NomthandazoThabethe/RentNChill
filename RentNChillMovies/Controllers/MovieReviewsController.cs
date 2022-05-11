using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentNChillMovies.Data;
using RentNChillMovies.Models;
using RentNChillMovies.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RentNChillMovies.Models
{
    public class MovieReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public MovieReviewsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        // GET: MovieReviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieReviews.Include(m => m.Movie).Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MovieReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieReview = await _context.MovieReviews
                .Include(m => m.Movie)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieReview == null)
            {
                return NotFound();
            }

            return View(movieReview);
        }

        // GET: MovieReviews/Create
        public IActionResult Create(int id)
        {
            ViewData["MovieId"] = id;
            
            return View();
        }


        // POST: MovieReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieReviewId,MovieId,UserId,ReviewDescription")] MovieReview movieReview)
        {
            var username = _context.MovieReviews.Where(n => n.UserId == movieReview.UserId);
            if (username.ToString() == movieReview.UserId)
            {
                ModelState.AddModelError("Email", "User with this email already exists");
                TempData["Fail"] = "You have already reviewed this movie!";
                return RedirectToAction("Details", "Movies", new { id = movieReview.MovieId });
            }

            else if (ModelState.IsValid)
            {

                movieReview.UserId = _userManager.GetUserId(User);
                
                ViewData["MovieId"] = movieReview.MovieId;
                _context.Add(movieReview);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Movies", new { id = movieReview.MovieId });
            }
            
            return View(movieReview);
        }

        // GET: MovieReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieReview = await _context.MovieReviews.FindAsync(id);
            if (movieReview == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "ImdbURL", movieReview.MovieId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", movieReview.UserId);
            return View(movieReview);
        }

        // POST: MovieReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieReviewId,MovieId,UserId,ReviewDescription")] MovieReview movieReview)
        {
            if (id != movieReview.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieReviewExists(movieReview.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "ImdbURL", movieReview.MovieId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", movieReview.UserId);
            return View(movieReview);
        }

        // GET: MovieReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieReview = await _context.MovieReviews
                .Include(m => m.Movie)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieReview == null)
            {
                return NotFound();
            }

            return View(movieReview);
        }

        // POST: MovieReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieReview = await _context.MovieReviews.FindAsync(id);
            _context.MovieReviews.Remove(movieReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieReviewExists(int id)
        {
            return _context.MovieReviews.Any(e => e.MovieId == id);
        }
    }
}
