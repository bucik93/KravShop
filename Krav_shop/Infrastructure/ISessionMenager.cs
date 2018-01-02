using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krav_shop.Infrastructure
{
    public interface ISessionMenager
    {
        //metoda pobranie elementu sesji 
        T Get<T>(string key);
        //metoda ustawienie sesji
        void Set<T>(string name, T value);
        //usuwanie elementu
        void Abandon();
        //metoda pobrania w TryGet 
        T TryGet<T>(string key);
    }
}
