namespace TestingsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRequireToModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answer", "AnswerName", c => c.String(nullable: false));
            AlterColumn("dbo.Question", "QuestionName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Question", "QuestionName", c => c.String());
            AlterColumn("dbo.Answer", "AnswerName", c => c.String());
        }
    }
}
