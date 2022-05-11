using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class MovieProducer
    {
        public int MovieProducerId { get; set; }
        public int ProducerId { get; set; }
        public int MovieId { get; set; }

        //Navigation Properties
        [ForeignKey("ProducerId")]
        public virtual Producer Producer { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}
