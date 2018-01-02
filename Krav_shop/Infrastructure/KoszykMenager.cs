using Krav_shop.DAL;
using Krav_shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Krav_shop.Infrastructure
{
    public class KoszykMenager
    {
        private ProduktyContext db;
        private ISessionMenager session;
        //stworzenie kostruktora
        public KoszykMenager(ISessionMenager session, ProduktyContext db)
        {
            this.session = session;
            this.db = db;
        }


        //metoda  pobierania zawartość koszyka
        public List<PozycjaKoszyka> PobierzKoszyk()
        {
            List<PozycjaKoszyka> koszyk;
            //sprawdzam czy się równa null jeśli jest null to w naszej sesji jest nic nie zapisane
            if(session.Get<List<PozycjaKoszyka>>(Consts.KoszykSessionKlucz)==null)
            {   //wtedy utwórz nową listę pozycji koszyka
                koszyk = new List<PozycjaKoszyka>();
            }
            else
            {   //jeśli w naszej sesji są elementy musimy je pobrać elementy koszyka z sesji
                koszyk = session.Get<List<PozycjaKoszyka>>(Consts.KoszykSessionKlucz) as List<PozycjaKoszyka>;
            }
            
            return koszyk;
        }
        //metoda dodaj do koszyka dodawać będzie produkt czyli id_produkt
        public void DodajDoKoszyka(int id_produkt)
        {   //tworzymy koszyk i pobierzemy koszyk (wszystko co jest w koszyku
            var koszyk = PobierzKoszyk();
            //zmienna pozycja koszyka (pobieramy z naszego koszyka czy mamy już taką pozycję o tym id które zostało przekazane będziemy sprawdzać
            var pozycjaKoszyka = koszyk.Find(k => k.Produkt.id_produkt == id_produkt);
            //jeżeli pozycja koszyka już istnieje to będziemy musieli zwiększyć ilośc a jeżeli pozycja koszyka nie istnieje to będziemy musieli dodać taką pozycję
            if (pozycjaKoszyka != null)
                pozycjaKoszyka.Ilosc++;
            else
            {   
                var produktDoDodania = db.Produkty.Where(k => k.id_produkt == id_produkt).SingleOrDefault();

                if(produktDoDodania != null)
                {
                    var nowaPozycjaKoszyka = new PozycjaKoszyka()
                    {
                        Produkt = produktDoDodania,
                        Ilosc = 1,
                        Wartosc = produktDoDodania.cena_produktu
                    };
                    koszyk.Add(nowaPozycjaKoszyka);
                }
            }
            //uaktualniamy pozycje w koszyku
            session.Set(Consts.KoszykSessionKlucz, koszyk);
        }
        //usuń z koszyka
        public int UsunZKoszyka(int id_produkt)
        {   //pobieram koszyk
            var koszyk = PobierzKoszyk();
            //pobieram pozycje z koszyka o id do usunięcia
            var pozycjaKoszyka = koszyk.Find(k => k.Produkt.id_produkt == id_produkt);
            //sprawdzam czy ta pozycja jest różna od null
            if(pozycjaKoszyka != null)
            {   //jeżeli jest to sprawdzamy ilość (jeżeli ilość jest większa od 1 
                if (pozycjaKoszyka.Ilosc > 1)
                {   //to zmniejsz tą ilość tylko o 1
                    pozycjaKoszyka.Ilosc--;
                    return pozycjaKoszyka.Ilosc;
                }
                else
                {   //jeżeli ilość koszyka nie jest większa od 1 czyli jest 1 to tą pozycję mamy całkowicie usunąć
                    koszyk.Remove(pozycjaKoszyka);
                }
            }
            return 0;
        }

        //pobierz wartość koszyka
        public decimal PobierzWartoscKoszyka()
        {   //pobieramy koszyk 
            var koszyk = PobierzKoszyk();
            /* bedę zwracać nasz koszyk (sumowanie) czyli dla każdego elementu koszyka będę sumował ilość razy cena produktu, 
            iteracja po każdym elemencie koszyka i pomnożymy ilość elementu koszyka razy cena kursu i to sumujemy czyli pobieram 
            całkowitą wartość koszyka */
            return koszyk.Sum(k => (k.Ilosc * k.Produkt.cena_produktu));
        }
        //zwraca ilosć pozycji koszyka
        public int PobierzIloscPozycjiKoszyka()
        {   //pobierz koszyk
            var koszyk = PobierzKoszyk();
            //na koszyku sumujemy tylko każdą pozycję ilosć 
            int ilosc = koszyk.Sum(k => k.Ilosc);
            return ilosc;

        }
        //tworzymy zamowienie
        public Zamowienie UtworzZamowienie(Zamowienie noweZamowienie, string id_user)
        {   //tworzymy koszyk i pobieramy wszystkie elementy jakie są w koszyku 
            var koszyk = PobierzKoszyk();
            //do nowego zamowienia ustawiamy datę czyli bieżącą
            noweZamowienie.data_dodania = DateTime.Now;
            noweZamowienie.id_user = id_user;
            //i następnie to zamówienie dodajemy do bazy
            db.Zamowienia.Add(noweZamowienie);
            // sprawdzam czy są pozycje zamówienia jesli są równie null to tworze wtedy liste zamówienia
            if (noweZamowienie.pozycje_zamowienia == null)
                //tworzymy liste pozycji zamowien
                noweZamowienie.pozycje_zamowienia = new List<Pozycja_zamowienia>();
              //wartość koszyka ustawiam na zero
            decimal koszykWartosc = 0;
            /*iteruje po wszystkich elementach koszyka czyli po wszystkich pozycjach, które zostały pobrane z sessji z naszego koszyka
               i dla każdej pozycji pobranej z naszego koszyka tworzymy nową pozycję zamowienia */
            foreach (var koszykElement in koszyk)
            {   //tworzymy nowa pozycje zamówienia dla każdego elementu koszyka
                var nowaPozycja_Zamowienia = new Pozycja_zamowienia()
                {
                    id_produkt = koszykElement.Produkt.id_produkt,
                    ilosc = koszykElement.Ilosc,
                    cena_zakupu = koszykElement.Produkt.cena_produktu
                };
                // koszyk wartość uaktualniamy o ilość razy cena 
                koszykWartosc += (koszykElement.Ilosc * koszykElement.Produkt.cena_produktu);
                //i tą nową pozycję zamówienia, którą stworzylismy  dodaję do pozycje zamowienia (do listy zamowienia)
                noweZamowienie.pozycje_zamowienia.Add(nowaPozycja_Zamowienia);

            }
            //na koniec uaktualniamy wartość zamówienia
            //pozycję zamowienia zapisujemy do bazy
            noweZamowienie.wartosc_zamowienia = koszykWartosc;
            db.SaveChanges();

            return noweZamowienie;

        }
        public void PustyKoszyk()
        {
            session.Set<List<PozycjaKoszyka>>(Consts.KoszykSessionKlucz, null);
        }

    }
   
}