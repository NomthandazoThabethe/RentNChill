using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class Rental
    {

        public int RentalId { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
        [DataType(DataType.Date)]
        public DateTime RentalDateStart { get; set; }
        [DataType(DataType.Date)]
        public DateTime RentalDateEnd { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double RentalAmount { get; set; }

        //navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

    }
}
