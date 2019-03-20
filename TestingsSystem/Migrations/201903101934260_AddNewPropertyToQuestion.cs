namespace TestingsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewPropertyToQuestion : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Question", name: "Test_TestID", newName: "Tests_TestID");
            RenameIndex(table: "dbo.Question", name: "IX_Test_TestID", newName: "IX_Tests_TestID");
            AddColumn("dbo.Question", "TestN", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Question", "TestN");
            RenameIndex(table: "dbo.Question", name: "IX_Tests_TestID", newName: "IX_Test_TestID");
            RenameColumn(table: "dbo.Question", name: "Tests_TestID", newName: "Test_TestID");
        }
    }
}
