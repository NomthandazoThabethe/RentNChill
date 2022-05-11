using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentNChillMovies.Data;
using RentNChillMovies.Models;
using RentNChillMovies.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Controllers
{
    public class MyMoviesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;

        public MyMoviesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: MyMoviesController
        public async Task <IActionResult> Index()
        {
            var user = userManager.GetUserId(User);
            var movies = await _context.UserMovies.Include(m => m.Movie).Where(r => r.UserId == user).ToListAsync() ;

            var myMoviesVm = new MyMoviesViewModel
            {
                Movies = movies
            };
            return View(myMoviesVm);
        }

       
    }
}
