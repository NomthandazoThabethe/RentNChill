using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentNChillMovies.Data;
using RentNChillMovies.Models;
using RentNChillMovies.ViewModels;
using System.Linq;
using System.Threading.Tasks;


namespace RentNChillMovies.Controllers
{
    public class NewUsersController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext _context;

        public NewUsersController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _context = context;
        }
        [Authorize(Policy = "writeonlypolicy")]
        public IActionResult NewUser()
        {
            ViewData["roles"] = roleManager.Roles.ToList();
            return View(new NewUserVM { });
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "writeonlypolicy")]
        public async Task<IActionResult> NewUser(NewUserVM newUserVM)
        {
            var role = roleManager.FindByIdAsync(newUserVM.Name).Result;
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName= newUserVM.User.Email,
                    Email = newUserVM.User.Email,
                    FirstName = newUserVM.User.FirstName,   
                    LastName = newUserVM.User.LastName,

                };

            // var userExists = await userManager.FindByNameAsync(user.UserName);
            
             
                var result = await userManager.CreateAsync(user,"Asdfghjkl;'456");
                _context.Add(user);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role.Name);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Movies");

            }
            return View( newUserVM); 
        }

    }
}
