using RentNChillMovies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.ViewModels
{
    public class MyMoviesViewModel
    {
        public UserMovie Movie { get; set; }
        public List<UserMovie> Movies { get; set; }
    }
}
