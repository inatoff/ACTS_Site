namespace ACTS.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relation_User_Teacher : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "UserKey", newName: "Teacher_TeacherId");
            RenameColumn(table: "dbo.Teachers", name: "TeacherKey", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.Teachers", name: "IX_TeacherKey", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_UserKey", newName: "IX_Teacher_TeacherId");
            DropColumn("dbo.Teachers", "UserKey");
            DropColumn("dbo.AspNetUsers", "TeacherKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "TeacherKey", c => c.Int());
            AddColumn("dbo.Teachers", "UserKey", c => c.Int());
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Teacher_TeacherId", newName: "IX_UserKey");
            RenameIndex(table: "dbo.Teachers", name: "IX_ApplicationUser_Id", newName: "IX_TeacherKey");
            RenameColumn(table: "dbo.Teachers", name: "ApplicationUser_Id", newName: "TeacherKey");
            RenameColumn(table: "dbo.AspNetUsers", name: "Teacher_TeacherId", newName: "UserKey");
        }
    }
}
