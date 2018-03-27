using SerwisOgloszen.BazaDanych;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerwisOgloszen.Models
{
    public class SzczegolyKonwersacjiViewModel
    {
        public List<Wiadomosc> ListaWiadomosci { get; set; }

        public Ogloszenie Ogloszenie { get; set; }

        [Display(Name ="Treść")]
        [Required(ErrorMessage ="Pole wymagane")]
        public string Tresc { get; set; }

        public long OgloszenieId { get; set; }

        public long OdbierajacyUzytkownikId { get; set; }
    }
}