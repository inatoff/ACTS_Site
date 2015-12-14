namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePropertyInNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Content", c => c.String(nullable: false));
            DropColumn("dbo.News", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "Description", c => c.String(nullable: false));
            DropColumn("dbo.News", "Content");
        }
    }
}
