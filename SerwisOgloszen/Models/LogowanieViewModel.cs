using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerwisOgloszen.Models
{
    public class LogowanieViewModel
    {
        //[Display(Name ="Twój Login")]
        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Niepoprawna ilość znaków")]
       /* [Compare("Haslo")] *//*sluzy np do porownywania hasel przy rejestracji*/
        public string Login { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Niepoprawna ilość znaków")]
        [Display(Name ="Hasło")]
        public string Haslo { get; set; }

        [Display(Name ="Zapamiętaj mnie")]
        public bool ZapamietajMnie { get; set; }

    }
}