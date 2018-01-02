using Krav_shop.DAL;
using Krav_shop.Infrastructure;
using Krav_shop.Models;
using Krav_shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Krav_shop.Controllers
{
    public class HomeController : Controller
    {
        private ProduktyContext db = new ProduktyContext();

        public ActionResult Index()
        {

            ICacheProvider cache = new DefaultCacheProvider();

            List<Kategoria> kategorie;
            if (cache.IsSet(Consts.KategorieCacheKey))
            {
                kategorie = cache.Get(Consts.KategorieCacheKey) as List<Kategoria>;
            }
            else
            {
                kategorie = db.Kategorie.ToList();
                cache.Set(Consts.KategorieCacheKey, kategorie, 60);
            }

            List<Produkt> nowosci;
            if (cache.IsSet(Consts.NowosciCacheKey))
            {
                nowosci = cache.Get(Consts.NowosciCacheKey) as List<Produkt>;
            }
            else
            {
                nowosci = db.Produkty.Where(a => !a.ukryty).OrderByDescending(a => a.data_dodania).Take(3).ToList();
                cache.Set(Consts.NowosciCacheKey, nowosci, 60);
            }

            List<Produkt> bestseller;
            if (cache.IsSet(Consts.BestsellerCacheKey))
            {
                bestseller = cache.Get(Consts.BestsellerCacheKey) as List<Produkt>;
            }
            else
            {
                bestseller = db.Produkty.Where(a => !a.ukryty && a.bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();
                cache.Set(Consts.BestsellerCacheKey, bestseller, 60);
            }
            var vm = new HomeViewModel()
            {
                Kategorie = kategorie,
                Nowosci = nowosci,
                Bestsellery = bestseller,
            };

            return View(vm);
        }
        public ActionResult StronyStatyczne(string nazwa)
        {
            return View(nazwa);
        }
    }
}
