namespace TestingsSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredToTestProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Test", "TestName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Test", "TestName", c => c.String());
        }
    }
}
