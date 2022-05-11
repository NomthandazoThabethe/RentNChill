using RentNChillMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Interfaces
{
    public interface IGenreRepository
    {
        Genre GetGenre(int genreId);
        Task PostNewGenre(Genre genre);
        Task UpdateGenre(Genre genre);
        
    }
}
