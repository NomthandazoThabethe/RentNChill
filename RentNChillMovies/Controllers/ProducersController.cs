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
    public class ProducersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment hostEnvironment;

        public ProducersController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }

        // GET: Producers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Producers.ToListAsync());
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = await _context.Producers
                .FirstOrDefaultAsync(m => m.ProducerId == id);
            var movies = await _context.MovieProducers.Where(m => m.ProducerId == id).Include(m => m.Movie).ToListAsync();
            if (producer == null)
            {
                return NotFound();
            }

            return View(new ProducerViewModel 
            { 
                Producer = producer,
                Movies = movies
            });
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProducerId,ProducerFirstName,ProducerLastName,ProducerBio,ProducerImage")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                if (producer != null)
                {
                    string wwwRootPath = hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(producer.ProducerThumbnail.FileName);
                    string extension = Path.GetExtension(producer.ProducerThumbnail.FileName);
                    fileName = DateTime.Now.ToString("yymmssffff") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/producerThumbnails/", fileName);
                    producer.ProducerImage = "/images/producerThumbnails/" + fileName;

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        producer.ProducerThumbnail.CopyTo(fileStream);
                    }
                }
                _context.Add(producer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = await _context.Producers.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProducerId,ProducerFirstName,ProducerLastName,ProducerBio")] Producer producer)
        {
            if (id != producer.ProducerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerExists(producer.ProducerId))
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
            return View(producer);
        }

        // GET: Producers/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = await _context.Producers
                .FirstOrDefaultAsync(m => m.ProducerId == id);
            var movies = await _context.MovieProducers.Where(m => m.ProducerId == id).Include(m => m.Movie).ToListAsync();
            if (producer == null)
            {
                return NotFound();
            }

            return View(new ProducerViewModel
            {
                Producer = producer,
                Movies = movies
            });
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = await _context.Producers.FindAsync(id);
            _context.Producers.Remove(producer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
            return _context.Producers.Any(e => e.ProducerId == id);
        }
    }
}
