using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class MovieReview
    {
        [Key]
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public string ReviewDescription { get; set; }

        //navigation properies
        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
        
        

    }
}
