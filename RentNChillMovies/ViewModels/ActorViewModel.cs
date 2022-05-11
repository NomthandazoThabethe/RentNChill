using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class ActorViewModel
    {
        public Actor Actor { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public Movie Movie { get; set; }
        public IEnumerable<MovieActor> Movies { get; set; }
    }
}
