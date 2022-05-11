using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentNChillMovies.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string UserId { get; set; }
       [Required]
       [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use letters only")]
        [Display(Name = "BankName")]
        public string BankName { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please use letters only")]
        [Display(Name = "AccountHolderName")]
        public string AccountHolderName { get; set; }
        [Required]
        //[StringLength(16, ErrorMessage = "The must be 16 Numbers long.", MinimumLength = 16)]
        //[DataType(DataType.CreditCard)]
        [Display(Name = "CardNumber")]

        public string CardNumber { get; set; }
        [Required]

        //[StringLength(3, ErrorMessage = "The must be 3 Numbers long.", MinimumLength = 3)]
        [Display(Name = "CVV")]


        public int CVV { get; set; }
        [Required]

        [Display(Name = "ExpireDate")]
        public int ExpireDate { get; set; }

        //navigation properties
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual List<Transaction> Transaction { get; set; }
    }
}
