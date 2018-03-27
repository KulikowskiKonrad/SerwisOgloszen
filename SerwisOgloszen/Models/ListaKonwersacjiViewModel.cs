using SerwisOgloszen.BazaDanych;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisOgloszen.Models
{
    public class ListaKonwersacjiViewModel
    {
        public long OgloszenieId { get; set; }

        public string TematOgloszenia { get; set; }

        public DateTime DataOstatniejWiadomosci { get; set; }

        public string LoginUzytkownika { get; set; }

        public long UzytkownikId { get; set; }

        public int IloscWiadomosci { get; set; }
    }
}