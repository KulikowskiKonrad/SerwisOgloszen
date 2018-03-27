using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerwisOgloszen.Models
{
    public class RejestracjaViewModel
    {

        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(20, MinimumLength = 4 ,ErrorMessage ="Niepoprawna ilość znaków")]
        public string Login { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Niepoprawna ilość znaków")]
        public string Haslo { get; set; }

        [Compare("Haslo", ErrorMessage = "Niezgodne hasła")]
        [Display(Name = "Powtórz hasło")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string PowtorzoneHaslo { get; set; }

    }
}