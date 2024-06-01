namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SerTables",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 30),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.ServiceId);
            
            AddColumn("dbo.MenDetTables", "ServiceId", c => c.Int(nullable: false));
            AlterColumn("dbo.MenDetTables", "Username", c => c.String());
            CreateIndex("dbo.MenDetTables", "ServiceId");
            AddForeignKey("dbo.MenDetTables", "ServiceId", "dbo.SerTables", "ServiceId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenDetTables", "ServiceId", "dbo.SerTables");
            DropIndex("dbo.MenDetTables", new[] { "ServiceId" });
            AlterColumn("dbo.MenDetTables", "Username", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.MenDetTables", "ServiceId");
            DropTable("dbo.SerTables");
        }
    }
}
