using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public int RentId { get; set; }
        //public int MovieId { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionAmount { get; set; }
        public bool IsCompleted { get; set; }

        //navigation properties
        [ForeignKey("AccountId")]
        public virtual Account Account{ get; set; }
        [ForeignKey("RentId")]
        public virtual Rental Rental { get; set; }
        //public virtual Movie Movie { get; set; }

    }
}
