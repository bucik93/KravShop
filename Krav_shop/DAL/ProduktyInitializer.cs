using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Krav_shop.Models;
using System.Data.Entity.Migrations;
using Krav_shop.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Krav_shop.DAL
{
    public class ProduktyInitializer : MigrateDatabaseToLatestVersion<ProduktyContext, Configuration>
    {

        public static void SeedProduktyData(ProduktyContext context)
        {
            var kategorie = new List<Kategoria>
            {
                new Kategoria() {id_kategoria = 1, nazwa_kat ="Koszulki", nazwa_pliku_ikony="", opis_kat= "opis koszulki"},
                new Kategoria() {id_kategoria = 2, nazwa_kat ="Bluzy", nazwa_pliku_ikony="", opis_kat= "opis bluzy"},
                new Kategoria() {id_kategoria = 3, nazwa_kat ="Spodnie", nazwa_pliku_ikony="", opis_kat= "opis spodni"},
                new Kategoria() {id_kategoria = 4, nazwa_kat ="Spodenki", nazwa_pliku_ikony="", opis_kat= "opis spodenek"},
                new Kategoria() {id_kategoria = 5, nazwa_kat ="Ochranicze i Kaski", nazwa_pliku_ikony="", opis_kat= "opis ochraniaczy"},
                new Kategoria() {id_kategoria = 6, nazwa_kat ="Rękawice Bokserskie", nazwa_pliku_ikony="", opis_kat= "opis rękawic bokserskich"},
                new Kategoria() {id_kategoria = 7, nazwa_kat ="Rękawice MMA", nazwa_pliku_ikony="", opis_kat= "opis rękawic mma"},
                new Kategoria() {id_kategoria = 8, nazwa_kat ="Tarcze i Łapy", nazwa_pliku_ikony="", opis_kat= "opis tarcz"},
                new Kategoria() {id_kategoria = 9, nazwa_kat ="Torby Sportowe i Plecaki", nazwa_pliku_ikony="", opis_kat= "opis torby sportowej"},
                new Kategoria() {id_kategoria = 10, nazwa_kat ="Czapki", nazwa_pliku_ikony="", opis_kat= "opis czapki"},
                new Kategoria() {id_kategoria = 11, nazwa_kat ="Akcesoria", nazwa_pliku_ikony="", opis_kat= "opis akcesoriów"},
                new Kategoria() {id_kategoria = 12, nazwa_kat ="Buty", nazwa_pliku_ikony="", opis_kat= "opis butów"},
            };
            kategorie.ForEach(k => context.Kategorie.AddOrUpdate(k));
            context.SaveChanges();
            var produkty = new List<Produkt>
            {
                //new Produkt() {id_produkt = 1,  nazwa_produktu = "Koszulka", id_kategoria = 1, cena_produktu = 50, bestseller = true, nazwa_pliku_obrazka = "kosulka.jpg",
                //data_dodania = DateTime.Now, opis="Koszulka treningowa2", ukryty = false},
                //new Produkt() {id_produkt = 2,  nazwa_produktu = "Koszulka3", id_kategoria = 1, cena_produktu = 50, bestseller = true, nazwa_pliku_obrazka = "kosulka.jpg",
                //data_dodania = DateTime.Now, opis="Koszulka treningowa Krav Maga z logiem Kravvtrening", ukryty = false},
                //new Produkt() {id_produkt = 3,  nazwa_produktu = "Koszulka Krav Maga Kravvtrening", id_kategoria = 1, cena_produktu = 50, bestseller = true, nazwa_pliku_obrazka = "kosulka.jpg",
                //data_dodania = DateTime.Now, opis="Koszulka treningowa Krav Maga z logiem Kravvtrening", ukryty = false},
                //new Produkt() {id_produkt = 4, id_kategoria = 1, nazwa_produktu ="Bluza Nike", opis= "Bluza treningowa",
                 //data_dodania = DateTime.Now, nazwa_pliku_obrazka = "bluza.jpg", cena_produktu = 100, bestseller = true , ukryty = false},
                 //new Produkt() {id_produkt = 5, id_kategoria = 3, nazwa_produktu ="Spodnie sportowe", opis= "Dresy do ćwiczeń",
                 //data_dodania = DateTime.Now, nazwa_pliku_obrazka = "spodnie.jpg", cena_produktu = 80, bestseller = true, ukryty = false },
                //  new Produkt() {id_produkt = 6, id_kategoria = 4, nazwa_produktu ="spodenki sportowe", opis= "Krótkie spodenki do ćwiczeń",
                // data_dodania = DateTime.Now, nazwa_pliku_obrazka = "spodenki.jpg", cena_produktu = 60, bestseller = true, ukryty = false },
                //new Produkt() {id_produkt = 7, id_kategoria = 5, nazwa_produktu ="tarcza treningowa", opis= "mała tarcza treningowa",
                // data_dodania = DateTime.Now, nazwa_pliku_obrazka = "małatarcza.jpg", cena_produktu = 80, bestseller = true , ukryty = false},
                //new Produkt() {id_produkt = 8, id_kategoria = 6,nazwa_produktu ="Rękawice bokserskie", opis= "Sparingowe rękawice bokserskie",
                // data_dodania = DateTime.Now, nazwa_pliku_obrazka = "rekawicebokserskie.jpg", cena_produktu = 150, bestseller = true, ukryty = false },
                //new Produkt() {id_produkt = 9, id_kategoria = 7, nazwa_produktu ="Rękawice MMA", opis= "Sparingowe rękawice MMA",
                // data_dodania = DateTime.Now, nazwa_pliku_obrazka = "rekawicemma.jpg", cena_produktu = 120, bestseller = true , ukryty = false},
                //new Produkt() {id_produkt = 10, id_kategoria = 8, nazwa_produktu ="tarcza treningowa", opis= "mała tarcza treningowa",
                // data_dodania = DateTime.Now, nazwa_pliku_obrazka = "małatarcza.jpg", cena_produktu = 90, bestseller = true, ukryty = false },
                //new Produkt() {id_produkt = 11, id_kategoria = 9, nazwa_produktu ="Torba sportowa", opis= "Torba sportowa",
                // data_dodania = DateTime.Now, nazwa_pliku_obrazka = "torba sportowa.jpg", cena_produktu = 190, bestseller = true, ukryty = false },
                //new Produkt() {id_produkt = 12, id_kategoria = 10, nazwa_produktu ="Czapka", opis= "Czapka",
                // data_dodania = DateTime.Now, nazwa_pliku_obrazka = "czapka.jpg", cena_produktu = 100, bestseller = true, ukryty = false },
                //new Produkt() {id_produkt = 13, id_kategoria = 11, nazwa_produktu ="Atrapa broni", opis= "broń treningowa",
                // data_dodania = DateTime.Now, nazwa_pliku_obrazka = "broń.jpg", cena_produktu = 130, bestseller = true , ukryty = false},
                //new Produkt() {id_produkt = 14, id_kategoria = 12, nazwa_produktu ="buty sportowe", opis= "buty sportowe",
                // data_dodania = DateTime.Now, nazwa_pliku_obrazka = "boty.jpg", cena_produktu = 80, bestseller = true , ukryty = false}
            };
            produkty.ForEach(k => context.Produkty.AddOrUpdate(k));
            context.SaveChanges();

        }

        public static void SeedUzytkownicy(ProduktyContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            const string name = "admin@test.pl";
            const string password = "123!@#QAZqaz";
            const string roleName = "Admin";

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, DaneUzytkownika = new DaneUzytkownika() };
                var result = userManager.Create(user, password);
            }

            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}