using SerwisOgloszen.BazaDanych;
using SerwisOgloszen.Models;
using SerwisOgloszen.Repozytoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SerwisOgloszen.Api
{
    public class OgloszenieApiController : ApiController
    {
        [HttpGet]
        [Authorize]
        public string Test()
        {
            return "ssdds";
        }

        [HttpGet]
        [Authorize]
        public List<OgloszenieViewModel> PobierzOgloszenia()
        {
            OgloszenieRepozytorium ogloszenieRepozytorium = new OgloszenieRepozytorium();
            List<Ogloszenie> listaOgloszen = ogloszenieRepozytorium.PobierzWszystkie();
            List<OgloszenieViewModel> listaOgloszenViewModel = new List<OgloszenieViewModel>();
            foreach (Ogloszenie ogloszenie in listaOgloszen)
            {
                listaOgloszenViewModel.Add(new OgloszenieViewModel()
                {
                    Id=ogloszenie.Id,
                    DataDodania=ogloszenie.DataDodania,
                    Temat=ogloszenie.Temat,
                    Tresc=ogloszenie.Tresc,
                    UzytkownikId=ogloszenie.UzytkownikId,
                    LoginUzytkownika=ogloszenie.Uzytkownik.Login,
                    NazwaKategorii=ogloszenie.Kategoria.Nazwa
                });
            }
            return listaOgloszenViewModel;
        }
    }
}
