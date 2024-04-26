namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCategTableAddAnnotation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CategoryTables", "Title", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.CategoryTables", "Description", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.CategoryTables", "Price_12");
            DropColumn("dbo.CategoryTables", "Price_6");
            DropColumn("dbo.CategoryTables", "Price_3");
            DropColumn("dbo.CategoryTables", "Price_1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CategoryTables", "Price_1", c => c.String(nullable: false));
            AddColumn("dbo.CategoryTables", "Price_3", c => c.String(nullable: false));
            AddColumn("dbo.CategoryTables", "Price_6", c => c.String(nullable: false));
            AddColumn("dbo.CategoryTables", "Price_12", c => c.String(nullable: false));
            AlterColumn("dbo.CategoryTables", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.CategoryTables", "Title", c => c.String(nullable: false));
        }
    }
}
