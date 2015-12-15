namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maybeItWorks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SecondName = c.String(),
                        Surname = c.String(),
                        Post = c.String(),
                        Photo = c.Binary(),
                        Degree = c.String(),
                        EMail = c.String(),
                        Intellect = c.String(),
                        VkID = c.String(),
                        FaceBook = c.String(),
                        Twitter = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Create = c.DateTime(),
                        Modified = c.DateTime(),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NewsID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News");
            DropTable("dbo.Employees");
        }
    }
}
