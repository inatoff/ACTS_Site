namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptionforNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Create", c => c.DateTime());
            AddColumn("dbo.News", "Modified", c => c.DateTime());
            AddColumn("dbo.News", "Description", c => c.String());
            DropColumn("dbo.News", "DataCreate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "DataCreate", c => c.DateTime());
            DropColumn("dbo.News", "Description");
            DropColumn("dbo.News", "Modified");
            DropColumn("dbo.News", "Create");
        }
    }
}
