using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{

    public class Movie
    {
        public int MovieId { get; set; }
        [DisplayName("Genre")]
        public int GenreId { get; set; }
        [DisplayName("Title")]
       // public int MovieRevieId { get; set; }
        public string MovieTitle { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Price { get; set; }
        [DisplayName("Synopsis")]
        public string MovieDescription { get; set; }
        [DisplayName("Audience Code")]
        public string AudienceCode { get; set; }
        [Url]
        [Required]
        [DisplayName("Imdb URL")]
        public string ImdbURL { get; set; }
        [DisplayName("Imdb Rating")]
        public double ImdbRating { get; set; }
        [Url]
        [Required]
        [DisplayName("Rotten Tomatoes URL")]
        public string RottenTomatoesURL { get; set; }
        [DisplayName("RottenTomatoes Rating")]
        public double RottenRating { get; set; }
        public string Language { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
        public string MovieThumbnail { get; set; }
        [Url]
        [Required]
        [DisplayName("Trailer")]
        public string MovieTrailer { get; set; }
        public bool IsAvailable { get; set; }
       

        //navigation properties
        [ForeignKey("GenreId")]
        public virtual Genre Genre{ get; set; }
        [ForeignKey("MovieReviewId")]
        //public virtual MovieReview MovieReview { get; set; }
        public virtual List<MovieReview> MovieReviews { get; set; }
        public virtual List<MovieProducer> Producers { get; set; }
        public virtual List<MovieActor> Actors { get; set; }
    }

    
}
