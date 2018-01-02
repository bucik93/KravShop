using Krav_shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Krav_shop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Kategoria> Kategorie {get; set;}
        public IEnumerable<Produkt> Nowosci { get; set; }
        public IEnumerable<Produkt> Bestsellery { get; set; }
     }
}