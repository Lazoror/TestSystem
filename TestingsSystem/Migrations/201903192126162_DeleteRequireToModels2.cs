namespace TestingsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRequireToModels2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answer", "AnswerName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Answer", "AnswerName", c => c.String(nullable: false));
        }
    }
}
