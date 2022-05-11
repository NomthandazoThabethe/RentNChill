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
    public class Producer
    {
        public int ProducerId { get; set; }
        [Required]
        [DisplayName("Name")]
        public string ProducerFirstName { get; set; }
        [Required]
        [DisplayName("Surname")]
        public string ProducerLastName { get; set; }
        public string ProducerFullName 
        {
            get { return ProducerFirstName + " " + ProducerLastName; }
        }
        [Required]
        [DisplayName("Bio")]
        public string ProducerBio { get; set; }
        [NotMapped]
        [DisplayName("Image")]
        public IFormFile ProducerThumbnail { get; set; }
        [DisplayName("Image")]
        public string ProducerImage { get; set; }

        //Navigation Properties
        public virtual List<Movie> Movies { get; set; }
    }
}
