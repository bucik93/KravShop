using Krav_shop.DAL;
using Krav_shop.Models;
using MvcSiteMapProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Krav_shop.Infrastructure
{
    public class KategorieDynamicNodeProvider : DynamicNodeProviderBase
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
        foreach (Kategoria kategoria in db.Kategorie)
        {   //Dla każdego produktu tworzę DynamicNodeProvider(deklaruję dynamicnodeprovider)
            DynamicNode node = new DynamicNode();
            //ustawiamy mu wartość Title nazwę produktu 
            node.Title = kategoria.nazwa_kat;
            //ustawiam klucz dla noda
            node.Key = "Kategoria_" + kategoria.id_kategoria;
           
            node.RouteValues.Add("nazwaKategori", kategoria.nazwa_kat);
            //przekazanie node do listy
            returnValue.Add(node);
        }
        //generuje dynamicznie linki (na koniec zwracam liste)
        return returnValue;
    }

}
}