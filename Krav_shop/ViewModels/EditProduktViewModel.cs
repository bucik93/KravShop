using Krav_shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Krav_shop.ViewModels
{
    public class EditProduktViewModel
    {
        public Produkt Produkt { get; set; }
        public IEnumerable<Kategoria> Kategorie { get; set; }
        public bool? Potwierdzenie { get; set; }
    }
}