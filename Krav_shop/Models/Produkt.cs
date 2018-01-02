using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Krav_shop.Models
{
    public class Produkt
    {
        [Key]
        public int id_produkt { get; set; }
        public int id_kategoria { get; set; }
        [Required(ErrorMessage ="Wprowadź nazwę produktu")]
        [StringLength(100)]
        public string nazwa_produktu { get; set; }
        public string opis { get; set; }
        public DateTime data_dodania { get; set; }
        [StringLength(100)]
        public string nazwa_pliku_obrazka { get; set; }
        public decimal cena_produktu { get; set; }
        public bool bestseller { get; set; }
        public bool ukryty { get; set; }
        //właściwość nawigacyjna 
        public virtual Kategoria Kategoria { get; set; }
    }
}