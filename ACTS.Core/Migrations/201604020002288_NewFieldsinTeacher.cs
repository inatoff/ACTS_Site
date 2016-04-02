namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFieldsinTeacher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "Greetings", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "Greetings");
        }
    }
}
