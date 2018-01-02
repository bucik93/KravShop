using Krav_shop.App_Start;
using Krav_shop.DAL;
using Krav_shop.Infrastructure;
using Krav_shop.Models;
using Krav_shop.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Krav_shop.Controllers
{
    public class KoszykController : Controller
    {
        private KoszykMenager KoszykMenager;
        private ISessionMenager sessionMenager { get; set; }
        private ProduktyContext db;

        public KoszykController()
        {
            db = new ProduktyContext();
            sessionMenager = new SessionMenager();
            KoszykMenager = new KoszykMenager(sessionMenager, db);
        }
        // GET: Koszyk
        public ActionResult Index()
        {
            var pozycjeKoszyka = KoszykMenager.PobierzKoszyk();
            var cenaCalkowita = KoszykMenager.PobierzWartoscKoszyka();

            KoszykViewModel koszykViewModel = new KoszykViewModel()
            {
                PozycjeKoszyka = pozycjeKoszyka,
                CenaCalkowita = cenaCalkowita
            };
            return View(koszykViewModel);
        }
        public ActionResult DodajDoKoszyka(int id)
        {
            KoszykMenager.DodajDoKoszyka(id);

            return RedirectToAction ("Index");
        }
        public int PobierzIloscElementowKoszyka()
        {
            return KoszykMenager.PobierzIloscPozycjiKoszyka();
        }
        public ActionResult UsunZKoszyka(int id_produkt)
        {
            int iloscPozycji = KoszykMenager.UsunZKoszyka(id_produkt);
            int iloscPozycjiKoszyka = KoszykMenager.PobierzIloscPozycjiKoszyka();
            decimal wartoscKoszyka = KoszykMenager.PobierzWartoscKoszyka();

            var wynik = new KoszykUsuwanieViewModel
            {
                IdPozycjiUsuwanej = id_produkt,
                IloscPozycjiUsuwanej = iloscPozycji,
                KoszykCenaCalkowita = wartoscKoszyka,
                KoszykIloscPozycji = iloscPozycjiKoszyka,
            };
            return Json(wynik);
        }
   

        public async Task<ActionResult> Zaplac()
        {   
         
           
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
             
                var zamowienie = new Zamowienie
                {   
                    imie = user.DaneUzytkownika.Imie,
                    nazwisko = user.DaneUzytkownika.Nazwisko,
                    adres = user.DaneUzytkownika.Adres,
                    miasto = user.DaneUzytkownika.Miasto,
                    kod_pocztowy = user.DaneUzytkownika.KodPocztowy,
                    e_mail = user.DaneUzytkownika.Email,
                    telefon = user.DaneUzytkownika.Telefon
                };
                return View(zamowienie);
            }

            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Zaplac", "Koszyk") });
        }
        [HttpPost]
        public async Task<ActionResult> Zaplac(Zamowienie zamowienieSzczegoly)
        {   
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                var newOrder = KoszykMenager.UtworzZamowienie(zamowienieSzczegoly, userId);

                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.DaneUzytkownika);
                await UserManager.UpdateAsync(user);
                
                KoszykMenager.PustyKoszyk();                              

                return RedirectToAction("PotwierdzenieZamowienia");
            }
            else
                return View(zamowienieSzczegoly);
        }

        public ActionResult PotwierdzenieZamowienia()
        {
            var name = User.Identity.Name;
           
            return View();
        }

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
    }
}
