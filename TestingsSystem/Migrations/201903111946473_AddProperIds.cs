namespace TestingsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProperIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answer", "Question_QuestionID", "dbo.Question");
            DropForeignKey("dbo.Question", "Tests_TestID", "dbo.Test");
            DropIndex("dbo.Answer", new[] { "Question_QuestionID" });
            DropIndex("dbo.Question", new[] { "Tests_TestID" });
            RenameColumn(table: "dbo.Answer", name: "Question_QuestionID", newName: "QuestionID");
            RenameColumn(table: "dbo.Question", name: "Tests_TestID", newName: "TestID");
            AlterColumn("dbo.Answer", "QuestionID", c => c.Int(nullable: false));
            AlterColumn("dbo.Question", "TestID", c => c.Int(nullable: false));
            CreateIndex("dbo.Answer", "QuestionID");
            CreateIndex("dbo.Question", "TestID");
            AddForeignKey("dbo.Answer", "QuestionID", "dbo.Question", "QuestionID", cascadeDelete: true);
            AddForeignKey("dbo.Question", "TestID", "dbo.Test", "TestID", cascadeDelete: true);
            DropColumn("dbo.Question", "TestN");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Question", "TestN", c => c.Int(nullable: false));
            DropForeignKey("dbo.Question", "TestID", "dbo.Test");
            DropForeignKey("dbo.Answer", "QuestionID", "dbo.Question");
            DropIndex("dbo.Question", new[] { "TestID" });
            DropIndex("dbo.Answer", new[] { "QuestionID" });
            AlterColumn("dbo.Question", "TestID", c => c.Int());
            AlterColumn("dbo.Answer", "QuestionID", c => c.Int());
            RenameColumn(table: "dbo.Question", name: "TestID", newName: "Tests_TestID");
            RenameColumn(table: "dbo.Answer", name: "QuestionID", newName: "Question_QuestionID");
            CreateIndex("dbo.Question", "Tests_TestID");
            CreateIndex("dbo.Answer", "Question_QuestionID");
            AddForeignKey("dbo.Question", "Tests_TestID", "dbo.Test", "TestID");
            AddForeignKey("dbo.Answer", "Question_QuestionID", "dbo.Question", "QuestionID");
        }
    }
}
