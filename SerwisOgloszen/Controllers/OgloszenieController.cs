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
    public class OgloszenieController : Controller
    {
        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        [Authorize]
        public ActionResult Zapisz(long? id)
        {
            try
            {
                ZapiszOgloszenieViewModel model = new ZapiszOgloszenieViewModel();
               
                if (id.HasValue) // to to samo co id != null
                {
                    OgloszenieRepozytorium ogloszenieRepozytorium = new OgloszenieRepozytorium();
                    Ogloszenie pobraneOgloszenie = ogloszenieRepozytorium.Pobierz(id.Value); //id.value pobiera wartosc z  typu ktory pozwala na null

                    model.Id = pobraneOgloszenie.Id;
                    model.Temat = pobraneOgloszenie.Temat;
                    model.Tresc = pobraneOgloszenie.Tresc;
                    model.KategoriaId = pobraneOgloszenie.KategoriaId;
                 
                    // throw new Exception("ZLe");
                }
                return View("ZapiszOgloszenie", model);
               
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Zapisz(ZapiszOgloszenieViewModel model)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    OgloszenieRepozytorium ogloszenieRepozytorium = new OgloszenieRepozytorium();
                    Ogloszenie ogloszenie = null;
                    if(model.Id.HasValue == true)
                    {
                        ogloszenie = ogloszenieRepozytorium.Pobierz(model.Id.Value);
                    }
                    else {
                        ogloszenie = new Ogloszenie()
                        {
                            UzytkownikId = ((Uzytkownik)Session["uzytkownik"]).Id,
                            DataDodania = DateTime.Now
                        };
                    }
                    ogloszenie.Temat = model.Temat;
                    ogloszenie.Tresc = model.Tresc;
                    ogloszenie.KategoriaId = model.KategoriaId.Value;
                    long? rezultatZapisania = ogloszenieRepozytorium.Zapisz(ogloszenie);
                    return RedirectToAction("ListaOgloszen2");
                }
                else
                {
                    return View("ZapiszOgloszenie", model);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ListaOgloszen()
        {
            try
            {
                OgloszenieRepozytorium ogloszenieRepozytorium = new OgloszenieRepozytorium();
                List<Ogloszenie> listaOgloszen = ogloszenieRepozytorium.PobierzWszystkie();
                return View(listaOgloszen); //musi zwrocic to co pobralem czyli liste
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }

        [HttpGet]
        public ActionResult ListaOgloszen2()
        {
            try
            {
                OgloszenieRepozytorium ogloszenieRepozytorium = new OgloszenieRepozytorium();
                List<Ogloszenie> listaOgloszen2 = ogloszenieRepozytorium.PobierzWszystkie();
                return View(listaOgloszen2); //musi zwrocic to co pobralem czyli liste
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Usun(long id)
        {
            try
            {
                OgloszenieRepozytorium ogloszenieRepozytorium = new OgloszenieRepozytorium();
                bool rezultatUsuniecia = ogloszenieRepozytorium.UsunOgloszenie(id);
                return RedirectToAction("ListaOgloszen2");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}