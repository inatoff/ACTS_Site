namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropCreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Created", c => c.DateTime());
            DropColumn("dbo.News", "Create");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "Create", c => c.DateTime());
            DropColumn("dbo.News", "Created");
        }
    }
}
