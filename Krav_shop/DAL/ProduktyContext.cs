using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Krav_shop.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Krav_shop.DAL
{
    public class ProduktyContext : IdentityDbContext<ApplicationUser>
    {
        public ProduktyContext() : base("ProduktyContext")
        {

        }

        static ProduktyContext()
        {
            Database.SetInitializer<ProduktyContext>(new ProduktyInitializer());
        }

        public static ProduktyContext Create()
        {
            return new ProduktyContext();
        }


        //definiowanie tabel
        public virtual DbSet<Produkt> Produkty { get; set; }
        public virtual DbSet<Kategoria> Kategorie { get; set; }
        public virtual DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<Pozycja_zamowienia> PozycjeZamowienia { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}