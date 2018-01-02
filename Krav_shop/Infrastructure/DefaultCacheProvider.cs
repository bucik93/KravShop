using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Krav_shop.Infrastructure
{
    public class DefaultCacheProvider : ICacheProvider
    {
        //dodaję właściwość Private cache 
        //pobieranie nasz wybrany cache
        private Cache cache {get { return HttpContext.Current.Cache;}}
        //metoda pobierająca cache
        public object Get(string key)
        {
            //metoda zwracająca cache i podajemy klucz
            return cache[key];
        }
        //ustawianie cache
        public void Set(string key, object data, int cacheTime)
        {
            //do bieżącego czasu dodajemy minuty
            var expirationTime = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            cache.Insert(key, data, null, expirationTime, Cache.NoSlidingExpiration);
        }
        //Sprawdzanie czy coś w cache jest ustawione
        public bool IsSet(string key)
        {
            //zwracamy 
            return (cache[key] != null);
        }
        //metoda usuwająca cache
        public void Invalidate(string key)
        {
            //wywołujemy na naszym cache metodę remowe i przekazujemy klucz
            cache.Remove(key);
        }
     
    }
}