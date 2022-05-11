
using RentNChillMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Interfaces
{
    public interface IMoviesRepository
    {
     
        Movie GetMovies(int movieId);
        Task UpdateMovies(MovieViewModel movieViewModel);
        Task PostNewMovie(MovieViewModel movieViewModel);
    }
}
