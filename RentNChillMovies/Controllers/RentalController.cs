using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentNChillMovies.Data;
using RentNChillMovies.Models;
using RentNChillMovies.ViewModels;

namespace RentNChillMovies.Controllers
{
    public class RentalController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;

        public RentalController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Rental
        [Authorize(Policy = "writeonlypolicy")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rentals.Include(r => r.Movie).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Rental/New Rental
        [Authorize(Policy = "readonlypolicy")]
        public IActionResult NewRental(int id)
        {
            var startDate = DateTime.Today.ToShortDateString();
            var endDate = DateTime.Today.AddDays(30).ToShortDateString();
            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            var movie = _context.Movies.FirstOrDefault(b => b.MovieId == id);
            return View(new RentalViewModel 
            {
                Movie = movie
            });
        }

        // POST: Rental/New Rental
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "readonlypolicy")]
        public  IActionResult NewRental(int id, Rental rent)
        {
            var user = userManager.GetUserId(User);
            var account = _context.Accounts.FirstOrDefault(a => a.UserId == user);
        
            var movie = _context.Movies.FirstOrDefault(b => b.MovieId == id);
            if (ModelState.IsValid)
            {
                if (account == null) 
                {
                    return RedirectToAction("Create", "Accounts", new { id = id });
                }
                else 
                {
                    return RedirectToAction("Transaction", "Transactions", new { id = id });
                }
               
                
            }
            return View(rent);
        }
        
    }
}
