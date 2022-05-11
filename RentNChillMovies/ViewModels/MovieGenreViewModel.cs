using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentNChillMovies.Models
{
    public class MovieGenreViewModel
    {
        public List<Movie> Movies { get; set; }
        public  List <UserLike> UserLikes { get; set; }
        public UserLike UserLike { get; set; }
        public SelectList Genres { get; set; }
        public string MovieGenre { get; set; }
        public string SearchString { get; set; }
        //public List<Actor> Actors { get; set; }
            
    }
}
 