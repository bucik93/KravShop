using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Krav_shop.Infrastructure
{
    public class Consts
    {   //jedna stała NowosciCacheKey, która przechowuje klucz NowosciCacheKey
        public const string NowosciCacheKey = "NowosciCacheKey";
        public const string BestsellerCacheKey = "BestsellerCacheKey";
        public const string KategorieCacheKey = "KategorieCacheKey";
        public const string KoszykSessionKlucz = "KoszykSessionKlucz";
    }
}