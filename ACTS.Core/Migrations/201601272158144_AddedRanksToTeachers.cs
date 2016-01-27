namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRanksToTeachers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teachers", "Rank", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Teachers", "Rank");
        }
    }
}
