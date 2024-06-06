namespace Spartacus.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        { 
            CreateTable(
                "dbo.RegisterTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 30),
                        Token = c.String(),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
           
        }
        
        public override void Down()
        {
            
            DropForeignKey("dbo.MenDetTables", "ServiceId", "dbo.SerTables");
            DropIndex("dbo.MenDetTables", new[] { "ServiceId" });
            DropTable("dbo.UTables");
            DropTable("dbo.ResetTokens");
            DropTable("dbo.RegisterTokens");
            DropTable("dbo.SerTables");
            DropTable("dbo.MenDetTables");
        }
    }
}
