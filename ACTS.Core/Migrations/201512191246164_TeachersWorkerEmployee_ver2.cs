namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeachersWorkerEmployee_ver2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "FullName", c => c.String());
            AddColumn("dbo.Employees", "Position", c => c.String());
            AddColumn("dbo.Employees", "Photo", c => c.Binary());
            AddColumn("dbo.Employees", "PhotoMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "PhotoMimeType");
            DropColumn("dbo.Employees", "Photo");
            DropColumn("dbo.Employees", "Position");
            DropColumn("dbo.Employees", "FullName");
        }
    }
}
