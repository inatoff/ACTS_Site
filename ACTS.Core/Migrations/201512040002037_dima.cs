namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dima : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Name", c => c.String());
            AddColumn("dbo.Employees", "SecondName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "SecondName");
            DropColumn("dbo.Employees", "Name");
        }
    }
}
