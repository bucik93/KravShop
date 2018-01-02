using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Krav_shop.Models
{
    public class Zamowienie
    {
        [Key]
        public int id_zamowienie { get; set; }

        public string id_user { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required(ErrorMessage = "Wprowadź imię")]
        [StringLength(50)]
        public string imie { get; set; }

        [Required(ErrorMessage = "Wprowadź nazwisko")]
        [StringLength(50)]
        public string nazwisko { get; set; }

        [Required(ErrorMessage = "Wprowadz adres")]
        [StringLength(100)]
        public string adres { get; set; }
        
        [Required(ErrorMessage = "Wprowadź miasto")]
        [StringLength(100)]
        public string miasto { get; set; }

        [Required(ErrorMessage = "Wprowadź kod pocztowy")]
        [StringLength(6)]
        public string kod_pocztowy { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić numer telefonu")]
        [StringLength(20)]
        [RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
        public string telefon { get; set; }

        [Required(ErrorMessage = "Wprowadź swój adres e-mail.")]
        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
        public string e_mail { get; set; }

        public string komentarz { get; set; }

        public DateTime data_dodania { get; set; }

        public stan_zamowienia stan_zamowienia { get; set; }

        public decimal wartosc_zamowienia { get; set; }

        public List<Pozycja_zamowienia> pozycje_zamowienia { get; set; }

    }
    //stan zamowienia albo nowe albo zrealizowane
    public enum stan_zamowienia
    {
        nowe,
        zrealizowane
    }
}