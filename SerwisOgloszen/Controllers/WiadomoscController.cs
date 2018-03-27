using SerwisOgloszen.BazaDanych;
using SerwisOgloszen.Models;
using SerwisOgloszen.Repozytoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SerwisOgloszen.Controllers
{
    public class WiadomoscController : Controller
    {

        [HttpGet]
        [Authorize]
        public ActionResult ListaKonwersacji()
        {
            try
            {
                WiadomoscRepozytorium wiadomoscRepozytorium = new WiadomoscRepozytorium();
                List<Wiadomosc> listaWiadomosci = wiadomoscRepozytorium.PobierzWszystkie(((Uzytkownik)Session["uzytkownik"]).Id);
                List<ListaKonwersacjiViewModel> konwersacjaViewModel = new List<ListaKonwersacjiViewModel>();
                foreach (Wiadomosc wiadomosc in listaWiadomosci)
                {
                    long uzytkownikId = wiadomosc.OdbierajacyUzytkownikId;
                    string loginUzytkownika = wiadomosc.UzytkownikOdbierajacy.Login;
                    if (uzytkownikId == ((Uzytkownik)Session["uzytkownik"]).Id)
                    {
                        uzytkownikId = wiadomosc.WysylajacyUzytkownikId;
                        loginUzytkownika = wiadomosc.UzytkownikWysylajacy.Login;
                    }
                    if (konwersacjaViewModel.Where(x => x.OgloszenieId == wiadomosc.OgloszenieId
                        && x.UzytkownikId == uzytkownikId).Any() == false)
                    {
                        konwersacjaViewModel.Add(new ListaKonwersacjiViewModel()
                        {
                            DataOstatniejWiadomosci = wiadomosc.DataDodania,
                            LoginUzytkownika = loginUzytkownika,
                            OgloszenieId = wiadomosc.OgloszenieId,
                            TematOgloszenia = wiadomosc.Ogloszenie.Temat,
                            UzytkownikId = uzytkownikId,
                            IloscWiadomosci = listaWiadomosci.Where(x => x.OgloszenieId == wiadomosc.OgloszenieId
                                && (x.WysylajacyUzytkownikId == uzytkownikId || x.OdbierajacyUzytkownikId == uzytkownikId)).Count()
                        });
                    }
                }
                return View(konwersacjaViewModel);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult WyslijWiadomosc(long ogloszenieId, long odbierajacyUzytkownikId) //ten parametre biore z actioon link
        {
            try
            {
                WiadomoscRepozytorium wiadomoscRepozytorium = new WiadomoscRepozytorium();
                OgloszenieRepozytorium ogloszenieRepozytorium = new OgloszenieRepozytorium();
                Ogloszenie ogloszenie = ogloszenieRepozytorium.Pobierz(ogloszenieId);
                List<Wiadomosc> listaWiadomosci = wiadomoscRepozytorium.Pobierz(((Uzytkownik)Session["uzytkownik"]).Id, odbierajacyUzytkownikId, ogloszenie.Id);// pobiera wszystkie z 3 parametrami ma byc 
                SzczegolyKonwersacjiViewModel szczegolyKonwersacjiViewModel = new SzczegolyKonwersacjiViewModel();
                szczegolyKonwersacjiViewModel.ListaWiadomosci = listaWiadomosci;
                szczegolyKonwersacjiViewModel.Ogloszenie = ogloszenie;
                szczegolyKonwersacjiViewModel.OdbierajacyUzytkownikId = odbierajacyUzytkownikId;
                szczegolyKonwersacjiViewModel.OgloszenieId = ogloszenieId;
                return View("SzczegolyKonwersacji", szczegolyKonwersacjiViewModel);
            }
            catch (Exception ex)
            {
                return View("ListaOgloszen2", "Ogloszenie");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult WyslijWiadomosc(SzczegolyKonwersacjiViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    WiadomoscRepozytorium wiadomoscRepozytorium = new WiadomoscRepozytorium();
                    Wiadomosc wiadomosc = new Wiadomosc();
                    wiadomosc.Tresc = model.Tresc;
                    wiadomosc.OdbierajacyUzytkownikId = model.OdbierajacyUzytkownikId;
                    wiadomosc.OgloszenieId = model.OgloszenieId;
                    wiadomosc.DataDodania = DateTime.Now;
                    wiadomosc.WysylajacyUzytkownikId = ((Uzytkownik)Session["uzytkownik"]).Id;
                    wiadomoscRepozytorium.Zapisz(wiadomosc);
                    return RedirectToAction("WyslijWiadomosc", new
                    {
                        ogloszenieId = model.OgloszenieId,
                        odbierajacyUzytkownikId = model.OdbierajacyUzytkownikId
                    });
                }
                else
                {
                    return RedirectToAction("WyslijWiadomosc", new
                    {
                        ogloszenieId = model.OgloszenieId,
                        odbierajacyUzytkownikId = model.OdbierajacyUzytkownikId
                    });
                }
            }
            catch
            {
                return View("Error");

            }
        }
    }
}