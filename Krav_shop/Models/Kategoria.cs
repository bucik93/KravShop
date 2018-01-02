using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Krav_shop.Models
    
{
    public class Kategoria
    {
        [Key]
        public int id_kategoria { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę kategorii")]
        [StringLength(100)]
        public string nazwa_kat { get; set; }
        [Required(ErrorMessage = "Wprowadź opis kategorii")]
        public string opis_kat { get; set; }
        public string nazwa_pliku_ikony { get; set; }
        //właściwość nawigacyjna
        //do kategori będzie można przypisywać wiele produktów
        public virtual ICollection<Produkt> Produkty { get; set; }

    }
}