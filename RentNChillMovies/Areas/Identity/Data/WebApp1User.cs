using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Areas.Identity.Data
{
    public class WebApp1User : IdentityUser
    {
        public String AccountName { get; set; }
        public String AccountHolderName { get; set; }
        public long CardNumber { get; set; }
        public int CVV { get; set; }
        public int ExpireDate { get; set; }
    }
}
