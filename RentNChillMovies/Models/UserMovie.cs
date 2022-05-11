using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class UserMovie
    {
        //[Key]
        public int MovieId { get; set; }
        public string UserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime RentalEndDate { get; set; }
        public bool IsTrasactionComplete { get; set; }
        
        //Navigation Properties
        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}
