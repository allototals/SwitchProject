namespace Switch.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial31 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schemes", "Route_Id", "dbo.Routes");
            DropIndex("dbo.Schemes", new[] { "Route_Id" });
            AddColumn("dbo.Routes", "SourceNode_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Schemes", "Route_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Routes", "SourceNode_Id");
            CreateIndex("dbo.Schemes", "Route_Id");
            AddForeignKey("dbo.Routes", "SinkNode_Id", "dbo.SinkNodes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Schemes", "Route_Id", "dbo.Routes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schemes", "Route_Id", "dbo.Routes");
            DropForeignKey("dbo.Routes", "SourceNode_Id", "dbo.SourceNodes");
            DropIndex("dbo.Schemes", new[] { "Route_Id" });
            DropIndex("dbo.Routes", new[] { "SourceNode_Id" });
            AlterColumn("dbo.Schemes", "Route_Id", c => c.Guid());
            DropColumn("dbo.Routes", "SourceNode_Id");
            CreateIndex("dbo.Schemes", "Route_Id");
            AddForeignKey("dbo.Schemes", "Route_Id", "dbo.Routes", "Id");
        }
    }
}
