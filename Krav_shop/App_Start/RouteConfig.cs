using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Krav_shop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
        name: "ProduktySzczegoly",
        url: "Produkt-{id}.html",
        defaults: new { controller = "Produkty", action = "Szczegoly" }
        );

            routes.MapRoute(
          name: "ProduktyList",
          url: "Kategoria/{nazwaKategori,}.html",
          defaults: new { controller = "Produkty", action = "Lista" }
          );

            routes.MapRoute(
           name: "StronyStatyczne",
           url: "strony/{nazwa}.html",
           defaults: new { controller = "Home", action = "StronyStatyczne" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
