using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class User: IdentityUser
    {
        //make the user derive from identity app..
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        public byte[] ProfilePicture { get; set; }
        //Navigation Property
        public virtual List<UserMovie> MyMovies { get; set; }
        
    }
}
