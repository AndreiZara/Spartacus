namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFirstLastName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UDbTables", newName: "UTables");
            AddColumn("dbo.UTables", "Firstname", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.UTables", "Lastname", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UTables", "Lastname");
            DropColumn("dbo.UTables", "Firstname");
            RenameTable(name: "dbo.UTables", newName: "UDbTables");
        }
    }
}
