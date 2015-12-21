namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requeredFilds : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "FullName", c => c.String(nullable: false, maxLength: 70));
            AlterColumn("dbo.Employees", "Position", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Teachers", "FullName", c => c.String(nullable: false, maxLength: 70));
            AlterColumn("dbo.Teachers", "Position", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Posts", "Content", c => c.String());
            AlterColumn("dbo.News", "Content", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.News", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Teachers", "Position", c => c.String());
            AlterColumn("dbo.Teachers", "FullName", c => c.String());
            AlterColumn("dbo.Employees", "Position", c => c.String());
            AlterColumn("dbo.Employees", "FullName", c => c.String());
        }
    }
}
