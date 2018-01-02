namespace Krav_shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zamowienia : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Zamowienie", name: "ApplicationUser_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Zamowienie", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            AddColumn("dbo.Zamowienie", "id_user", c => c.String());
            AddColumn("dbo.Zamowienie", "adres", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Zamowienie", "telefon", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Zamowienie", "e_mail", c => c.String(nullable: false));
            DropColumn("dbo.Zamowienie", "ulica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Zamowienie", "ulica", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Zamowienie", "e_mail", c => c.String());
            AlterColumn("dbo.Zamowienie", "telefon", c => c.String());
            DropColumn("dbo.Zamowienie", "adres");
            DropColumn("dbo.Zamowienie", "id_user");
            RenameIndex(table: "dbo.Zamowienie", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Zamowienie", name: "User_Id", newName: "ApplicationUser_Id");
        }
    }
}
