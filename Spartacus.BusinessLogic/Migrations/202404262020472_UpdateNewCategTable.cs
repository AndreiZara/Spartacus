namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNewCategTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoryTables", "Price_12", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.CategoryTables", "Price_6", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.CategoryTables", "Price_3", c => c.String(nullable: false, maxLength: 10));
            AddColumn("dbo.CategoryTables", "Price_1", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CategoryTables", "Price_1");
            DropColumn("dbo.CategoryTables", "Price_3");
            DropColumn("dbo.CategoryTables", "Price_6");
            DropColumn("dbo.CategoryTables", "Price_12");
        }
    }
}
