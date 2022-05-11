using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace RentNChillMovies.Models
{
    public class MovieActor
    {
        public int MovieActorId { get; set; }
        public int ActorId { get; set; }
        public int MovieId { get; set; }
        //navigation properties
        [ForeignKey("ActorId")]
        public virtual Actor Actor { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}
