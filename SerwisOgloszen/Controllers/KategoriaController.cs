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
    public class KategoriaController : Controller
    {
        // GET: Kategoria
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult ListaKategorii()
        {
            try
            {
                KategoriaRepozytorium kategoriaRepozytorium = new KategoriaRepozytorium();
                List<Kategoria> listaKategorii = kategoriaRepozytorium.PobierzWszystkie();
                return View(listaKategorii);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpGet]
        //[Authorize(Roles = "Administrator")]
        [Authorize]
        public ActionResult Zapisz(long? id)
        {
            try
            {
                ZapiszKategorieViewModel model = new ZapiszKategorieViewModel();

                if (id.HasValue) // to to samo co id != null
                {
                    KategoriaRepozytorium kategoriaRepozytorium = new KategoriaRepozytorium();
                    Kategoria pobraneKategorie = kategoriaRepozytorium.Pobierz(id.Value); //id.value pobiera wartosc z  typu ktory pozwala na null

                    model.Id = pobraneKategorie.Id;
                    model.Nazwa = pobraneKategorie.Nazwa;
                    model.CzyUsunieta = pobraneKategorie.CzyUsunieta;
                    // throw new Exception("ZLe");
                }
                return View("ZapiszKategorie", model);

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Zapisz(ZapiszKategorieViewModel model)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    KategoriaRepozytorium kategoriaRepozytorium = new KategoriaRepozytorium();
                    Kategoria kategoria = null;
                    if (model.Id.HasValue == true)
                    {
                        kategoria = kategoriaRepozytorium.Pobierz(model.Id.Value);
                        kategoria.Nazwa = model.Nazwa;
                        kategoria.CzyUsunieta = model.CzyUsunieta;
                    }
                    else
                    {
                        kategoria = new Kategoria()
                        {
                            CzyUsunieta = model.CzyUsunieta,
                            Nazwa = model.Nazwa
                        };
                    }
                    long? rezultatZapisania = kategoriaRepozytorium.Zapisz(kategoria);
                    return RedirectToAction("ListaKategorii");
                }
                else
                {
                    return View("ZapiszKategorie", model);
                }
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
                KategoriaRepozytorium kategoriaRepozytorium = new KategoriaRepozytorium();
                bool rezultatUsuniecia = kategoriaRepozytorium.UsunKategorie(id);
                return RedirectToAction("ListaKategorii");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

    }
}