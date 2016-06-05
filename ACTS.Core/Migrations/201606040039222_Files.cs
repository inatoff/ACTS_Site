namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "PhotoId", c => c.Guid());
            AddColumn("dbo.Employees", "PhotoId", c => c.Guid());
            AddColumn("dbo.Events", "ImageId", c => c.Guid());
            AddColumn("dbo.News", "ImageId", c => c.Guid());
            DropColumn("dbo.Teachers", "Photo");
            DropColumn("dbo.Teachers", "PhotoMimeType");
            DropColumn("dbo.Employees", "Photo");
            DropColumn("dbo.Employees", "PhotoMimeType");
            DropColumn("dbo.Events", "ImageData");
            DropColumn("dbo.Events", "ImageMimeType");
            DropColumn("dbo.News", "ImageData");
            DropColumn("dbo.News", "ImageMimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.News", "ImageMimeType", c => c.String());
            AddColumn("dbo.News", "ImageData", c => c.Binary());
            AddColumn("dbo.Events", "ImageMimeType", c => c.String());
            AddColumn("dbo.Events", "ImageData", c => c.Binary());
            AddColumn("dbo.Employees", "PhotoMimeType", c => c.String());
            AddColumn("dbo.Employees", "Photo", c => c.Binary());
            AddColumn("dbo.Teachers", "PhotoMimeType", c => c.String());
            AddColumn("dbo.Teachers", "Photo", c => c.Binary());
            DropColumn("dbo.News", "ImageId");
            DropColumn("dbo.Events", "ImageId");
            DropColumn("dbo.Employees", "PhotoId");
            DropColumn("dbo.Teachers", "PhotoId");
        }
    }
}
