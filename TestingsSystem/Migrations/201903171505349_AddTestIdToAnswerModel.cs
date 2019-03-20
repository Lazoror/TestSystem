namespace TestingsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestIdToAnswerModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answer", "TestID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answer", "TestID");
        }
    }
}
