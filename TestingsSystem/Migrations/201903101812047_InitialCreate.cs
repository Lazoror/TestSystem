namespace TestingsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        AnswerID = c.Int(nullable: false, identity: true),
                        IsTrueAnswer = c.Boolean(nullable: false),
                        Question_QuestionID = c.Int(),
                    })
                .PrimaryKey(t => t.AnswerID)
                .ForeignKey("dbo.Question", t => t.Question_QuestionID)
                .Index(t => t.Question_QuestionID);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        QuestionName = c.String(),
                        AnswerCount = c.Int(nullable: false),
                        Test_TestID = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.Test", t => t.Test_TestID)
                .Index(t => t.Test_TestID);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        TestID = c.Int(nullable: false, identity: true),
                        TestName = c.String(),
                        TestObject = c.String(),
                        QuestionCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Question", "Test_TestID", "dbo.Test");
            DropForeignKey("dbo.Answer", "Question_QuestionID", "dbo.Question");
            DropIndex("dbo.Question", new[] { "Test_TestID" });
            DropIndex("dbo.Answer", new[] { "Question_QuestionID" });
            DropTable("dbo.Test");
            DropTable("dbo.Question");
            DropTable("dbo.Answer");
        }
    }
}
