namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChUser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UDbTables", newName: "UTables");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UTables", newName: "UDbTables");
        }
    }
}
