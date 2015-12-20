namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeachersWorkerEmployee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherID = c.Int(nullable: false, identity: true),
                        Degree = c.String(),
                        EMail = c.String(),
                        Intellect = c.String(),
                        VkID = c.String(),
                        FaceBook = c.String(),
                        Twitter = c.String(),
                        FullName = c.String(),
                        Position = c.String(),
                        Photo = c.Binary(),
                        PhotoMimeType = c.String(),
                    })
                .PrimaryKey(t => t.TeacherID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 500),
                        Create = c.DateTime(),
                        Modified = c.DateTime(),
                        Content = c.String(nullable: false),
                        Teacher_TeacherID = c.Int(),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Teachers", t => t.Teacher_TeacherID)
                .Index(t => t.Teacher_TeacherID);
            
            DropColumn("dbo.Employees", "Name");
            DropColumn("dbo.Employees", "SecondName");
            DropColumn("dbo.Employees", "Surname");
            DropColumn("dbo.Employees", "Post");
            DropColumn("dbo.Employees", "Photo");
            DropColumn("dbo.Employees", "Degree");
            DropColumn("dbo.Employees", "EMail");
            DropColumn("dbo.Employees", "Intellect");
            DropColumn("dbo.Employees", "VkID");
            DropColumn("dbo.Employees", "FaceBook");
            DropColumn("dbo.Employees", "Twitter");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "Twitter", c => c.String());
            AddColumn("dbo.Employees", "FaceBook", c => c.String());
            AddColumn("dbo.Employees", "VkID", c => c.String());
            AddColumn("dbo.Employees", "Intellect", c => c.String());
            AddColumn("dbo.Employees", "EMail", c => c.String());
            AddColumn("dbo.Employees", "Degree", c => c.String());
            AddColumn("dbo.Employees", "Photo", c => c.Binary());
            AddColumn("dbo.Employees", "Post", c => c.String());
            AddColumn("dbo.Employees", "Surname", c => c.String());
            AddColumn("dbo.Employees", "SecondName", c => c.String());
            AddColumn("dbo.Employees", "Name", c => c.String());
            DropForeignKey("dbo.Posts", "Teacher_TeacherID", "dbo.Teachers");
            DropIndex("dbo.Posts", new[] { "Teacher_TeacherID" });
            DropTable("dbo.Posts");
            DropTable("dbo.Teachers");
            DropTable("dbo.Events");
        }
    }
}
