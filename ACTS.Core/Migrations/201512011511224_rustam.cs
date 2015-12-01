namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rustam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
