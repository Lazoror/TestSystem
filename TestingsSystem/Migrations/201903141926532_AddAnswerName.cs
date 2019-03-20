namespace TestingsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnswerName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answer", "AnswerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answer", "AnswerName");
        }
    }
}
