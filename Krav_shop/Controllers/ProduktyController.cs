
using Krav_shop.DAL;
using Krav_shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Krav_shop.Controllers
{
    public class ProduktyController : Controller
    {
        private ProduktyContext db = new ProduktyContext();

        // GET: Produkty
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Lista(string nazwaKategori)
        {

            var kategoria = db.Kategorie.Include("Produkty").Where(k => k.nazwa_kat.ToUpper() == nazwaKategori.ToUpper()).SingleOrDefault();

            var produkty = kategoria.Produkty.ToList();


            return View(kategoria.Produkty.ToList().AsEnumerable());
        }

        public ActionResult Szczegoly(int id)
        {  
            var produkt = db.Produkty.Find(id);
            return View(produkt);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60000)]
        public ActionResult KategorieMenu()
        {
            var kategorie = db.Kategorie.ToList();
            return PartialView("_KategorieMenu", kategorie);
        }

        public ActionResult ProduktyPodpowiedzi(string term)
        {  
            var produkty = db.Produkty.Where(a => !a.ukryty && a.nazwa_produktu.ToLower().Contains(term.ToLower()))
                      
                        .Take(5).Select(a => new { label = a.nazwa_produktu });
          
            return Json(produkty, JsonRequestBehavior.AllowGet);
        }
        
     
    }
}