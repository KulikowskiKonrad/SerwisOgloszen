using SerwisOgloszen.BazaDanych;
using SerwisOgloszen.Repozytoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerwisOgloszen.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Request.IsAuthenticated==true && Session["uzytkownik"]== null)
            {
                //long idUzytkownika=long.Parse(Request.LogonUserIdentity.UserClaims.Where(x => x.Type == "UserId").Single().Value);
                string login = User.Identity.Name;
                UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                Uzytkownik uzytkownik = uzytkownikRepozytorium.Pobierz(login);
                Session["uzytkownik"] = uzytkownik;
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}