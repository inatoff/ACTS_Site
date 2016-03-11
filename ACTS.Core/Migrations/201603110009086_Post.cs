namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Post : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "Slug", c => c.String());
            AddColumn("dbo.Posts", "Created", c => c.DateTime());
            AddColumn("dbo.Posts", "CombinedTags", c => c.String());
            DropColumn("dbo.Posts", "Create");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Create", c => c.DateTime());
            DropColumn("dbo.Posts", "CombinedTags");
            DropColumn("dbo.Posts", "Created");
            DropColumn("dbo.Posts", "Slug");
        }
    }
}
