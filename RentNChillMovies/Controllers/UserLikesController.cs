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

namespace RentNChillMovies.Controllers
{
    public class UserLikesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;

        public UserLikesController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: UserLikes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserLikes.Include(u => u.Movie).Include(u => u.User);
            return View(await applicationDbContext.ToListAsync());
        }

        
        [Authorize(Policy = "readonlypolicy")]
        public async Task<IActionResult> Create(int Id,string isLike,string isDislike, UserLike userLike)
        {
            var user = userManager.GetUserId(User);
            var userlikes = _context.UserLikes.Where(r => r.UserId == user)
                   .FirstOrDefault(r => r.MovieId == Id);
            if ((isDislike!=null)||(isLike!=null))
            {
                if (userlikes!=null) 
                {
                    _context.UserLikes.Remove(userlikes);
                    await _context.SaveChangesAsync();
                }
                
                if (isDislike=="True")
                    {
                        var userLikes = new UserLike
                        {
                            UserId = user,
                            MovieId = Id,
                            IsLike = false,
                            IsDislike = true

                        };
                        _context.Add(userLikes);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Movies", new { id = Id });

                };
                 if(isLike=="True")
                    {
                        var userLikes = new UserLike
                        {
                            UserId = user,
                            MovieId = Id,
                            IsLike = true,
                            IsDislike = false

                        };
                        _context.Add(userLikes);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Movies", new { id = Id });
                };


            }
          
            return RedirectToAction("Details", "Movies", new { id = Id });
        }



     
    }
}
