namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeLastIpLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UDbTables", "LastIp", c => c.String(maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UDbTables", "LastIp", c => c.String(maxLength: 30));
        }
    }
}
