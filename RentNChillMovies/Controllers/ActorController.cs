using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentNChillMovies.Data;
using RentNChillMovies.Models;

namespace RentNChillMovies.Controllers
{
    public class ActorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public ActorController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: Actor
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Actors.ToListAsync());
        }

        // GET: Actor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .FirstOrDefaultAsync(m => m.ActorId == id);
            var movies = await _context.MovieActors.Where(m => m.ActorId == id).Include(m => m.Movie).ToListAsync();
            if (actor == null)
            {
                return NotFound();
            }

            return View(new ActorViewModel
            {
                Actor = actor,
                Movies = movies
            });
        }

        // GET: Actor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorId,ActorName,ActorLastName,ActorBio,ActorImage")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                if (actor != null)
                {
                    string wwwRootPath = hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(actor.ActorThumbnail.FileName);
                    string extension = Path.GetExtension(actor.ActorThumbnail.FileName);
                    fileName = DateTime.Now.ToString("yymmssffff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/producerThumbnails/", fileName);
                    actor.ActorImage = "/images/producerThumbnails/" + fileName;

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        actor.ActorThumbnail.CopyTo(fileStream);
                    }
                }
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        // GET: Actor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            return View(actor);
        }

        // POST: Actor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile ActorThumbnail, [Bind("ActorId,ActorName,ActorLastName,ActorBio,ActorImage")] Actor actor)
        {
            if (id != actor.ActorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (ActorThumbnail != null)
                {
                    string uploadFolder = Path.Combine(hostEnvironment.WebRootPath + "/images/actorThumbails");
                    string fileName = Guid.NewGuid().ToString() + "_" + ActorThumbnail.FileName;
                    string filePath = Path.Combine(uploadFolder, fileName);
                    ActorThumbnail.CopyTo(new FileStream(filePath, FileMode.Create));
                    actor.ActorImage = "/images/actorThumbails/" + fileName;
                }
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.ActorId))
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
            return View(actor);
        }

        // GET: Actor/Delete/5
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
               .FirstOrDefaultAsync(m => m.ActorId == id);
            var movies = await _context.MovieActors.Where(m => m.ActorId == id).Include(m => m.Movie).ToListAsync();
            if (actor == null)
            {
                return NotFound();
            }

            return View(new ActorViewModel
            {
                Actor = actor,
                Movies = movies
            });
        }

        // POST: Actor/Delete/5
       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.ActorId == id);
        }
    }
}
