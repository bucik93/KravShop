using Krav_shop.App_Start;
using Krav_shop.DAL;
using Krav_shop.Infrastructure;
using Krav_shop.Models;
using Krav_shop.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Krav_shop.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ProduktyContext db = new ProduktyContext();

        public enum ManageMessageId
        {  
            ChangePasswordSuccess,
            Error
        }

        //public ManageController(ProduktyContext context)
        //{
        //    this.db = context;
        //    //this.mailService = mailService;
        //}

        //public ManageController(ApplicationUserManager userManager)
        //{
        //    UserManager = userManager;
        //}

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {   
            var name = User.Identity.Name;

            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }
            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var model = new ManageCredentialsViewModel
            {
                Message = message,
                DaneUzytkownika = user.DaneUzytkownika
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "DaneUzytkownika")]DaneUzytkownika daneUzytkownika)
        {
            if (ModelState.IsValid)
            {   
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.DaneUzytkownika = daneUzytkownika;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }
            if (!ModelState.IsValid)
            {  
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        public ActionResult ListaZamowien()
        {
            var name = User.Identity.Name;
            //logger.Info("Admin zamowienia | " + name);
            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;
            IEnumerable<Zamowienie> zamowieniaUzytkownika;

         
            if (isAdmin)
            {
                zamowieniaUzytkownika = db.Zamowienia.Include("pozycje_zamowienia").OrderByDescending(o => o.data_dodania).ToArray();
            }
            else
            {   
                var userId = User.Identity.GetUserId();
                zamowieniaUzytkownika = db.Zamowienia.Where(o => o.id_user == userId).Include("pozycje_zamowienia").OrderByDescending(o => o.data_dodania).ToArray();
            }
            return View(zamowieniaUzytkownika);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public stan_zamowienia ZmianaStanuZamowienia(Zamowienie zamowienie)
        {  
            Zamowienie zamowienieDoModyfikacji = db.Zamowienia.Find(zamowienie.id_zamowienie);
           
            zamowienieDoModyfikacji.stan_zamowienia = zamowienie.stan_zamowienia;
         
            db.SaveChanges();

            //if (zamowienieDoModyfikacji.stan_zamowienia == stan_zamowienia.zrealizowane)
            //{
            //    this.mailService.WyslanieZamowienieZrealizowaneEmail(zamowienieDoModyfikacji);
            //}
            return zamowienie.stan_zamowienia;
        }

        [Authorize(Roles = "Admin")]
        //
        public ActionResult DodajProdukt(int? id_produkt, bool? potwierdzenie)
        {
            Produkt produkt;
            if (id_produkt.HasValue)
            {  
                ViewBag.EditMode = true;
              
                produkt = db.Produkty.Find(id_produkt);
            }
            else
            {   
                ViewBag.EditMode = false;
              
                produkt = new Produkt();
            }

            var result = new EditProduktViewModel();
            
            result.Kategorie = db.Kategorie.ToList();
            result.Produkt = produkt;
            result.Potwierdzenie = potwierdzenie;

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DodajProdukt(EditProduktViewModel model, HttpPostedFileBase file)
        {   
            if (model.Produkt.id_produkt > 0)
            {
                
                db.Entry(model.Produkt).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DodajProdukt", new { potwierdzenie = true });
            }
            else
            {
         
                if (file != null && file.ContentLength > 0)
                {  
                    if (ModelState.IsValid)
                    {
                        var fileExt = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid() + fileExt;
                        var path = Path.Combine(Server.MapPath(AppConfig.ObrazkiFolderWzgledny), filename);
                        file.SaveAs(path);
                        model.Produkt.nazwa_pliku_obrazka = filename;
                        model.Produkt.data_dodania = DateTime.Now;
                        db.Entry(model.Produkt).State = EntityState.Added;
                        db.SaveChanges();
                        return RedirectToAction("DodajProdukt", new { potwierdzenie = true });
                    }
                    else
                    {
                        var kategorie = db.Kategorie.ToList();
                        model.Kategorie = kategorie;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku");
                    var kategorie = db.Kategorie.ToList();
                    model.Kategorie = kategorie;
                    return View(model);
                }
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult UkryjProdukt(int id_produkt)
        {  
            var produkt = db.Produkty.Find(id_produkt);
            
            produkt.ukryty = true;
            db.SaveChanges();
            
            return RedirectToAction("DodajProdukt", new { potwierdzenie = true });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult PokazProdukt(int id_produkt)
        {
            var produkt = db.Produkty.Find(id_produkt);
           
            produkt.ukryty = false;
            db.SaveChanges();

            return RedirectToAction("DodajProdukt", new { potwierdzenie = true });
        }

        //[AllowAnonymous]
        //public ActionResult WyslaniePotwierdzenieZamowieniaEmail(int zamowienieId, string nazwisko)
        //{
        //    var zamowienie = db.Zamowienia.Include("PozycjeZamowienia").Include("PozycjeZamowienia.Kurs")
        //                       .SingleOrDefault(o => o.ZamowienieID == zamowienieId && o.Nazwisko == nazwisko);

        //    if (zamowienie == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

        //    PotwierdzenieZamowieniaEmail email = new PotwierdzenieZamowieniaEmail();
        //    email.To = zamowienie.Email;
        //    email.From = "mariuszjurczenko@gmail.com";
        //    email.Wartosc = zamowienie.WartoscZamowienia;
        //    email.NumerZamowienia = zamowienie.ZamowienieID;
        //    email.PozycjeZamowienia = zamowienie.PozycjeZamowienia;
        //    email.sciezkaObrazka = AppConfig.ObrazkiFolderWzgledny;
        //    email.Send();

        //    return new HttpStatusCodeResult(HttpStatusCode.OK);
        //}

        //[AllowAnonymous]
        //public ActionResult WyslanieZamowienieZrealizowaneEmail(int zamowienieId, string nazwisko)
        //{         
        //    var zamowienie = db.Zamowienia.Include("PozycjeZamowienia").Include("PozycjeZamowienia.Kurs")
        //                          .SingleOrDefault(o => o.ZamowienieID == zamowienieId && o.Nazwisko == nazwisko);

        //    if (zamowienie == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

        //    ZamowienieZrealizowaneEmail email = new ZamowienieZrealizowaneEmail();
        //    email.To = zamowienie.Email;
        //    email.From = "mariuszjurczenko@gmail.com";
        //    email.NumerZamowienia = zamowienie.ZamowienieID;
        //    email.Send();

        //    return new HttpStatusCodeResult(HttpStatusCode.OK);
        //}
    }
}