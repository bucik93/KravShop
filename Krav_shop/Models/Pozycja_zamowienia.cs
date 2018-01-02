using System.ComponentModel.DataAnnotations;

namespace Krav_shop.Models
{
    public class Pozycja_zamowienia
    {
        [Key]
        public int id_pozycja_zamowienia { get; set; }
        public int id_zamowienie { get; set; }
        public int id_produkt { get; set; }
        public int ilosc { get; set; }
        //np. cena może być po rabacie
        public decimal cena_zakupu { get; set; }

        public virtual Produkt Produkt { get; set; }
        public virtual Zamowienie Zamowienie { get; set; }
    }
}