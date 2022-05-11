using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RentNChillMovies.Models;

namespace RentNChillMovies.ViewModels
{
    public class NewUserVM
    {

        public User User { get; set; }
       
        [Display(Name = "Role")]
        public string Name { get; set; }
      
       
    }
}
