namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UTables", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UTables", "FileName");
        }
    }
}
