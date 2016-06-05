namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderedItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Disciplines",
                c => new
                    {
                        OrderedItemId = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderedItemId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        OrderedItemId = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderedItemId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        OrderedItemId = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderedItemId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.ScienceInterests",
                c => new
                    {
                        OrderedItemId = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderedItemId)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScienceInterests", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Publications", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Projects", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Disciplines", "TeacherId", "dbo.Teachers");
            DropIndex("dbo.ScienceInterests", new[] { "TeacherId" });
            DropIndex("dbo.Publications", new[] { "TeacherId" });
            DropIndex("dbo.Projects", new[] { "TeacherId" });
            DropIndex("dbo.Disciplines", new[] { "TeacherId" });
            DropTable("dbo.ScienceInterests");
            DropTable("dbo.Publications");
            DropTable("dbo.Projects");
            DropTable("dbo.Disciplines");
        }
    }
}
