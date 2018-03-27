using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisOgloszen.Models
{
    public class OgloszenieViewModel
    {

        public long Id { get; set; }
        public string Tresc { get; set; }
        public long UzytkownikId { get; set; }
        public System.DateTime DataDodania { get; set; }
        public string Temat { get; set; }
        public string NazwaKategorii { get; set; }
        public string LoginUzytkownika { get; set; }
    }
}