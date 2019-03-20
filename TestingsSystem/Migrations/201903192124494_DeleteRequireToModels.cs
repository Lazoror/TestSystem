namespace TestingsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRequireToModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Question", "QuestionName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Question", "QuestionName", c => c.String(nullable: false));
        }
    }
}
