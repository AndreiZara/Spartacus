namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenDetTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        Username = c.String(),
                        Description = c.String(nullable: false, maxLength: 30),
                        Activity = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SerTables", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenDetTables", "ServiceId", "dbo.SerTables");
            DropIndex("dbo.MenDetTables", new[] { "ServiceId" });
            DropTable("dbo.UTables");
            DropTable("dbo.ResetTokens");
            DropTable("dbo.SerTables");
            DropTable("dbo.MenDetTables");
        }
    }
}
