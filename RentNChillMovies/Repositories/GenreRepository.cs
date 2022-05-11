using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RentNChillMovies.Data;
using RentNChillMovies.Interfaces;
using RentNChillMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext dbContext;

        public GenreRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Genre GetGenre(int genreId)
        {

            return dbContext.Genres.FirstOrDefault(b => b.GenreId == genreId);
        }

        public async Task PostNewGenre(Genre genre)
        {
            if (genre == null)
            {
                throw new ArgumentNullException(nameof(genre));
            }
            dbContext.Add(genre);
            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            
        }

        public async Task UpdateGenre(Genre genre)
        {
            if (genre == null)
            {
                throw new ArgumentNullException(nameof(genre));
            }
            var genreNewName = GetGenre(genre.GenreId);
            genreNewName.GenreName = genre.GenreName;

            dbContext.Genres.Update(genre);

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }

    
}
