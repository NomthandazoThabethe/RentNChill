using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class ProducerViewModel
    {
        public Producer Producer { get; set; }
        public IEnumerable<Producer> Producers { get; set; }
        public Movie Movie { get; set; }
        public IEnumerable<MovieProducer> Movies { get; set; }
    }
}
