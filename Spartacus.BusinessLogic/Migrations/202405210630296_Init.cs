namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UTables", "ImageData");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UTables", "ImageData", c => c.Binary());
        }
    }
}
