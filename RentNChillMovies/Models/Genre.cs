using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        [DisplayName("Genre")]
        public string GenreName { get; set; }

        //Navigation Property
        public virtual List<Movie> Movies { get; set; }
    }
}
