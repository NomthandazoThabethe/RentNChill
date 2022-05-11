using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class MovieViewModel
    {
        public List<int> Actor { get; set; }
        public List<UserLike> UserLikes { get; set; }
        public List<int> Producer { get; set; }
        public Actor Act { get; set; }
        public Movie Movie { get; set; }
        public UserLike UserLike { get; set; }
        public MovieReview MovieReview { get; set; }
        public List<Actor> Actors { get; set; }
        public List<Producer> Producers { get; set; }
        public IEnumerable<MovieActor> movieActors { get; set; }
        public IEnumerable<MovieProducer> movieProducers { get; set; }
        public List<User> Users { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public int TotalVotes { get; set; }
        public int PercentageVotes { get; set; }
        public IEnumerable<MovieReview> movieReviews { get; set; }

    }
}
