using SerwisOgloszen.BazaDanych;
using SerwisOgloszen.Enums;
using SerwisOgloszen.Models;
using SerwisOgloszen.Repozytoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SerwisOgloszen.Helpers;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;

namespace SerwisOgloszen.Controllers
{
    public class UzytkownikController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Uzytkownik
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Anuluj()
        {
            try
            {
                UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                List<Uzytkownik> listaUzytkownikow = uzytkownikRepozytorium.PobierzWszystkich();
                return RedirectToAction("ListaUzytkownikow", listaUzytkownikow);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edytuj(long id)
        {
            try
            {
                UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                Uzytkownik uzytkownik = uzytkownikRepozytorium.Pobierz(id);
                EdytujViewModel edytujViewModel = new EdytujViewModel()
                {
                    Id = uzytkownik.Id,
                    Login = uzytkownik.Login,
                    Rola = (RolaUzytkownika)uzytkownik.Rola
                };

                return View(edytujViewModel);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edytuj(EdytujViewModel model)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                    Uzytkownik uzytkownik = uzytkownikRepozytorium.Pobierz(model.Id);
                    uzytkownik.Rola = (byte)model.Rola;
                    if (string.IsNullOrEmpty(model.Haslo) == false)
                    {
                        uzytkownik.Haslo = model.Haslo;
                    }
                    long? rezultatEdycji = uzytkownikRepozytorium.Zapisz(uzytkownik);
                    if (rezultatEdycji != null)
                    {
                        return RedirectToAction("ListaUzytkownikow");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return View("Edytuj", model);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Usun(long id)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                    bool rezultatUsuniecia = uzytkownikRepozytorium.Usun(id);
                    return RedirectToAction("ListaUzytkownikow");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult ListaUzytkownikow()
        {
            try
            {
                UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                List<Uzytkownik> listaUzytkownikow = uzytkownikRepozytorium.PobierzWszystkich();
                return View("ListaUzytkownikow", listaUzytkownikow);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }



        [HttpGet]
        public ActionResult Zaloguj()
        {
            try
            {
                UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                List<Uzytkownik> listaUzytkownikow = uzytkownikRepozytorium.PobierzWszystkich();
                //throw new Exception(); wlasny wyjatek do sprawdzenia
                return View("Logowanie");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Zaloguj(LogowanieViewModel model)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    var result = new ApplicationSignInManager(HttpContext.GetOwinContext()).PasswordSignIn(model);

                    switch (result)
                    {
                        case SignInStatus.Success:
                            var tokenExpiration = TimeSpan.FromDays(1);
                            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                            identity.AddClaim(new Claim(ClaimTypes.Name, model.Login));
                            var props = new AuthenticationProperties()
                            {
                                IssuedUtc = DateTime.UtcNow,
                                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
                            };
                            var ticket = new AuthenticationTicket(identity, props);
                            var accessToken = Startup.OAuthServerOptions.AccessTokenFormat.Protect(ticket);
                            HttpCookie ciasteczkoZTokenem = new HttpCookie("tokenOAuth", "Bearer " + accessToken);
                            Response.Cookies.Add(ciasteczkoZTokenem);


                            return RedirectToAction("ListaOgloszen2", "Ogloszenie");

                        case SignInStatus.LockedOut:
                        case SignInStatus.RequiresVerification:
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("Haslo", "Niepoprawny login lub hasło");
                            return View("Logowanie", model);
                    }

                }
                else
                {
                    return View("Logowanie", model);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        [HttpPost]
        public ActionResult ZewnetrzneLogowanie(string provider)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ZewnetrzneLogowanieCallback"));
        }

        public async Task<ActionResult> ZewnetrzneLogowanieCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Zaloguj");
            }


            var result = new ApplicationSignInManager(HttpContext.GetOwinContext()).LogowanieZewnetrzne(loginInfo.DefaultUserName);

            switch (result)
            {
                case SignInStatus.Success:
                    var tokenExpiration = TimeSpan.FromDays(1);
                    ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, loginInfo.DefaultUserName));
                    var props = new AuthenticationProperties()
                    {
                        IssuedUtc = DateTime.UtcNow,
                        ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
                    };
                    var ticket = new AuthenticationTicket(identity, props);
                    var accessToken = Startup.OAuthServerOptions.AccessTokenFormat.Protect(ticket);
                    HttpCookie ciasteczkoZTokenem = new HttpCookie("tokenOAuth", "Bearer " + accessToken);
                    Response.Cookies.Add(ciasteczkoZTokenem);


                    return RedirectToAction("ListaOgloszen2", "Ogloszenie");

                case SignInStatus.LockedOut:
                case SignInStatus.RequiresVerification:
                case SignInStatus.Failure:
                default:
                    return RedirectToAction("Zaloguj");
            }
            // Sign in the user with this external login provider if the user already has a login
            //var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToLocal(returnUrl);
            //    case SignInStatus.LockedOut:
            //        return View("Lockout");
            //    case SignInStatus.RequiresVerification:
            //        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
            //    case SignInStatus.Failure:
            //    default:
            //        // If the user does not have an account, then prompt the user to create an account
            //        ViewBag.ReturnUrl = returnUrl;
            //        ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
            //        return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            //}
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        return RedirectToAction("Index", "Manage");
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        var info = await AuthenticationManager.GetExternalLoginInfoAsync();
        //        if (info == null)
        //        {
        //            return View("ExternalLoginFailure");
        //        }
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await UserManager.AddLoginAsync(user.Id, info.Login);
        //            if (result.Succeeded)
        //            {
        //                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
        //                return RedirectToLocal(returnUrl);
        //            }
        //        }
        //        AddErrors(result);
        //    }

        //    ViewBag.ReturnUrl = returnUrl;
        //    return View(model);
        //}

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_userManager != null)
        //        {
        //            _userManager.Dispose();
        //            _userManager = null;
        //        }

        //        if (_signInManager != null)
        //        {
        //            _signInManager.Dispose();
        //            _signInManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
        //}

        [HttpPost]
        public ActionResult Wyloguj()
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    if (Request.IsAuthenticated == true)
                    {
                        Session.Abandon();
                        HttpContext.GetOwinContext().Authentication.SignOut();
                    }
                    return RedirectToAction("Zaloguj");
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpGet] // get jest dla tego ze uzytkownik chce pobrac tylko widok i tyle
        public ActionResult Zarejestruj()
        {
            // ModelState.AddModelError("MyDropDownListKey", "Please Select");
            return View("Rejestracja");
        }

        [HttpPost]
        public ActionResult Zarejestruj(RejestracjaViewModel model)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    UzytkownikRepozytorium uzytkownikRepozytorium = new UzytkownikRepozytorium();
                    Uzytkownik pobranyUzytkownik = uzytkownikRepozytorium.Pobierz(model.Login);
                    if (pobranyUzytkownik == null)
                    {
                        string sol = Guid.NewGuid().ToString(); //robie sol jako GUID i zamieniam na string

                        Uzytkownik uzytkownik = new Uzytkownik()
                        {
                            Sol = sol,
                            Login = model.Login,
                            Haslo = MD5Helper.GenerujMD5(model.Haslo + sol), //generujemy md5 z polaczenia hasla i soli (losowego ciagu znakow) wywoluje metode statyczna z klasy
                                                                             //MD5Helper
                            Rola = (byte)RolaUzytkownika.Uzytkownik
                        };

                        long? rezultatZapisu = uzytkownikRepozytorium.Zapisz(uzytkownik);

                        if (rezultatZapisu != null)
                        {
                            return RedirectToAction("ListaOgloszen2", "Ogloszenie");
                        }

                        else
                        {
                            return View("Error");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Login", "Login jest już zajęty");
                        return View("Rejestracja", model);
                    }
                }
                else
                {
                    return View("Rejestracja", model);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }

    }
}
