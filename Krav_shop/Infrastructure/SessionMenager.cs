using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Krav_shop.Infrastructure
{
    public class SessionMenager : ISessionMenager
    {
        //stworzenie pola sesji 
        private HttpSessionState session;
        public SessionMenager()
        {
            session = HttpContext.Current.Session;
        }
        //pobieram sesje zwraca sessje
        public T Get<T>(string key)
        {
            return (T)session[key];
        }
        //ustawiamy sesje. Do sesji przypisujemy wartość
        public void Set<T>(string name, T value)
        {
            session[name] = value;
        }
        //usuwanie sesji
        public void Abandon()
        {
            session.Abandon();
        }
        //pobieranie sessji w try catchu
        public T TryGet<T>(string key)
        {
            try
            {
                return (T)session[key];
            }
            catch (NullReferenceException)
            {
                return default(T);
            }
        }
    }
}