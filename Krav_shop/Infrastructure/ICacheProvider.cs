using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krav_shop.Infrastructure
{
    public interface ICacheProvider
    {   // metoda będzie pobierała dane z cache, potrzebuje stringa klucz 
         object Get(string key);
        //następnie metoda do umieszczania danych w cache pobierająca string klucz i object data i pobierać czas.
        void Set(string key, object data, int cacheTime);
        //metoda, która będzie zwracać bool czy cache jest ustawiony i bedzie przyjmowac klucz. Metoda sprawdzająca czy coś w cache się znajduje. 
        bool IsSet(string key);
        //metoda do usuwania danych z cache
        void Invalidate(string key);
    }
}
