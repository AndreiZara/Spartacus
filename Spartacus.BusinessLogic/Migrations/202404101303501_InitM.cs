namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UDbTables", "Firstname", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.UDbTables", "Lastname", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.UDbTables", "LastIp", c => c.String(maxLength: 30));
            DropColumn("dbo.UDbTables", "LasIp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UDbTables", "LasIp", c => c.String(maxLength: 30));
            DropColumn("dbo.UDbTables", "LastIp");
            DropColumn("dbo.UDbTables", "Lastname");
            DropColumn("dbo.UDbTables", "Firstname");
        }
    }
}
