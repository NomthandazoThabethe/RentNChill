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
    public class Actor
    {
        public int ActorId { get; set; }
        [Required]
        [DisplayName("Name")]
        public String ActorName { get; set; }
        [Required]
        [DisplayName("Surname")]
        public String ActorLastName { get; set; }
        public String ActorFullName 
        {
            get { return ActorName + " " + ActorLastName; }
        }
        [Required]
        [DisplayName("Bio")]
        public String ActorBio { get; set; }
        [NotMapped]
        [DisplayName("Image")]
        public IFormFile ActorThumbnail { get; set; }
        [DisplayName("Image")]
        public string ActorImage { get; set; }

        //navigation properties
 
        public virtual List<MovieActor> MovieActors { get; set; }
    }
}
