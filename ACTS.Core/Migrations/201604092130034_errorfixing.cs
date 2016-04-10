namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class errorfixing : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Teachers", "RozkladId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teachers", "RozkladId", c => c.Int(nullable: false));
        }
    }
}
