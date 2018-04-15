namespace Switch.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial32 : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Routes", "SinkNode_Id", "dbo.SinkNodes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "SourceNode_Id");
        }
    }
}
