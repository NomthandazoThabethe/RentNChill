using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentNChillMovies.Models;

namespace RentNChillMovies.ViewModels
{
    public class TransactionViewModel
    {
        public Transaction Transaction{ get; set; }
        public Movie Movie { get; set; }
        public Account Account { get; set; }

        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public double MoviePrice { get; set; }

        public string AccountHodler { get; set; }



    }
}
