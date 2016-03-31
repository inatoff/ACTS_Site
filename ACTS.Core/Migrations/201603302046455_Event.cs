namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Event : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Created", c => c.DateTime());
            AddColumn("dbo.Events", "Modified", c => c.DateTime());
            AddColumn("dbo.Events", "StartView", c => c.DateTime());
            AddColumn("dbo.Events", "EndView", c => c.DateTime());
            AddColumn("dbo.Events", "UrlSlug", c => c.String());
            AddColumn("dbo.Events", "Title", c => c.String(nullable: false, maxLength: 500));
            AddColumn("dbo.Events", "ImageData", c => c.Binary());
            AddColumn("dbo.Events", "ImageMimeType", c => c.String());
            AddColumn("dbo.Events", "Content", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Content");
            DropColumn("dbo.Events", "ImageMimeType");
            DropColumn("dbo.Events", "ImageData");
            DropColumn("dbo.Events", "Title");
            DropColumn("dbo.Events", "UrlSlug");
            DropColumn("dbo.Events", "EndView");
            DropColumn("dbo.Events", "StartView");
            DropColumn("dbo.Events", "Modified");
            DropColumn("dbo.Events", "Created");
        }
    }
}
