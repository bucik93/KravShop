namespace Krav_shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategoria",
                c => new
                    {
                        id_kategoria = c.Int(nullable: false, identity: true),
                        nazwa_kat = c.String(nullable: false, maxLength: 100),
                        opis_kat = c.String(nullable: false),
                        nazwa_pliku_ikony = c.String(),
                    })
                .PrimaryKey(t => t.id_kategoria);
            
            CreateTable(
                "dbo.Produkt",
                c => new
                    {
                        id_produkt = c.Int(nullable: false, identity: true),
                        id_kategoria = c.Int(nullable: false),
                        nazwa_produktu = c.String(nullable: false, maxLength: 100),
                        opis = c.String(),
                        data_dodania = c.DateTime(nullable: false),
                        nazwa_pliku_obrazka = c.String(maxLength: 100),
                        cena_produktu = c.Decimal(nullable: false, precision: 18, scale: 2),
                        bestseller = c.Boolean(nullable: false),
                        ukryty = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_produkt)
                .ForeignKey("dbo.Kategoria", t => t.id_kategoria, cascadeDelete: true)
                .Index(t => t.id_kategoria);
            
            CreateTable(
                "dbo.Pozycja_zamowienia",
                c => new
                    {
                        id_pozycja_zamowienia = c.Int(nullable: false, identity: true),
                        id_zamowienie = c.Int(nullable: false),
                        id_produkt = c.Int(nullable: false),
                        ilosc = c.Int(nullable: false),
                        cena_zakupu = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id_pozycja_zamowienia)
                .ForeignKey("dbo.Produkt", t => t.id_produkt, cascadeDelete: true)
                .ForeignKey("dbo.Zamowienie", t => t.id_zamowienie, cascadeDelete: true)
                .Index(t => t.id_zamowienie)
                .Index(t => t.id_produkt);
            
            CreateTable(
                "dbo.Zamowienie",
                c => new
                    {
                        id_zamowienie = c.Int(nullable: false, identity: true),
                        imie = c.String(nullable: false, maxLength: 50),
                        nazwisko = c.String(nullable: false, maxLength: 50),
                        ulica = c.String(nullable: false, maxLength: 100),
                        miasto = c.String(nullable: false, maxLength: 100),
                        kod_pocztowy = c.String(nullable: false, maxLength: 6),
                        telefon = c.String(),
                        e_mail = c.String(),
                        komentarz = c.String(),
                        data_dodania = c.DateTime(nullable: false),
                        stan_zamowienia = c.Int(nullable: false),
                        wartosc_zamowienia = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id_zamowienie);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pozycja_zamowienia", "id_zamowienie", "dbo.Zamowienie");
            DropForeignKey("dbo.Pozycja_zamowienia", "id_produkt", "dbo.Produkt");
            DropForeignKey("dbo.Produkt", "id_kategoria", "dbo.Kategoria");
            DropIndex("dbo.Pozycja_zamowienia", new[] { "id_produkt" });
            DropIndex("dbo.Pozycja_zamowienia", new[] { "id_zamowienie" });
            DropIndex("dbo.Produkt", new[] { "id_kategoria" });
            DropTable("dbo.Zamowienie");
            DropTable("dbo.Pozycja_zamowienia");
            DropTable("dbo.Produkt");
            DropTable("dbo.Kategoria");
        }
    }
}
