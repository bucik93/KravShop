using Krav_shop.DAL;
using Krav_shop.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Dynamiczny node dla produktów
namespace Krav_shop.Infrastructure
{   //klasa do wyświetlania ścieżki nawigacyjnej w produktach
    public class ProduktySzczegolyDynamicNodeProvider : DynamicNodeProviderBase
    {
        //deklaracja kontekstu
        private ProduktyContext db = new ProduktyContext();
        //przeciążenie metody 
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode nodee)
        {
            //deklaracja zmniennej
            var returnValue = new List<DynamicNode>();
            //do listy dodaję produkty iteruję po wszystkich produktach
            //pobieram wszystkie produkty 
            foreach(Produkt produkt in db.Produkty)
            {   //Dla każdego produktu tworzę DynamicNodeProvider(deklaruję dynamicnodeprovider)
                DynamicNode node = new DynamicNode();
                //ustawiamy mu wartość Title nazwę produktu 
                node.Title = produkt.nazwa_produktu;
                //ustawiam klucz dla noda
                node.Key = "Produkt_" + produkt.id_produkt;
                //deklaruję klucz rodzica czyli kategoria (nadrzedny klucz
                node.ParentKey = "Kategoria_" + produkt.id_kategoria;
                node.RouteValues.Add("id", produkt.id_produkt);
                //przekazanie node do listy
                returnValue.Add(node);
            }
            //generuje dynamicznie linki (na koniec zwracam liste)
            return returnValue;
        }

    }
}