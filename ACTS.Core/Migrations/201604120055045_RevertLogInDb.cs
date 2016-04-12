namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RevertLogInDb : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.LogEntries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LogEntries",
                c => new
                    {
                        LogEntryId = c.Int(nullable: false, identity: true),
                        CallSite = c.String(nullable: false),
                        UtcDate = c.DateTime(nullable: false),
                        Exception = c.String(),
                        Level = c.Byte(nullable: false),
                        Logger = c.String(nullable: false, maxLength: 80),
                        MachineName = c.String(nullable: false),
                        Message = c.String(maxLength: 256),
                        StackTrace = c.String(nullable: false),
                        Thread = c.String(nullable: false, maxLength: 10),
                        Username = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.LogEntryId);
            
        }
    }
}
