using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentNChillMovies.Models;


namespace RentNChillMovies.ViewModels
{
    public class MovieReviewViewModel
    {
        public MovieReview MovieReview { get; set; }
        public Movie Movie { get; set; }
        public User User { get; set; }
        public int MovieId { get; set; }
        public int MovieReviewId { get; set; }
        public string UserId { get; set; }
        public string ReviewDescription { get; set; }

        //public IEnumerable<MovieActor> movieActors { get; set; }
        //public IEnumerable<MovieProducer> movieProducers { get; set; }
    }
}
