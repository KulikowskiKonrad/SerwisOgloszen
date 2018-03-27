using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerwisOgloszen.Models
{
    public class WyslijWiadomoscViewModel
    {
        [Display(Name = "Treść")]
        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(2000, ErrorMessage = "Niepoprawna ilość znaków")]
        public string Tresc { get; set; }

        public long OgloszenieId { get; set; }

        //public long UzykownikWysylajacyId { get; set; }

        //public long UzytkownikOdbierajacyId { get; set; }

        //public bool CzyOdczytano { get; set; }

    }
}