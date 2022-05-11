using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentNChillMovies.Data;
using RentNChillMovies.Interfaces;
using RentNChillMovies.Models;

namespace RentNChillMovies.Controllers
{
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenreRepository _genre;

        public GenresController(ApplicationDbContext context, IGenreRepository _genre)
        {
            this._genre = _genre;
            _context = context;
        }

        // GET: Genres
        public IActionResult Index()
        {
            var genres = _context.Genres.ToList();
           
            return View(genres);
        }

        // GET: Genres/Details/5
        public IActionResult Details(int id)
        {
            var genre = _genre.GetGenre(id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
               
                await _genre.PostNewGenre(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public IActionResult Edit(int id)
        {
            var genre = _genre.GetGenre(id);
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Genre genre)
        {

            if (ModelState.IsValid)
            {

                await _genre.UpdateGenre(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var genre = _genre.GetGenre(id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.GenreId == id);
        }
    }
}
