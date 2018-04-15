namespace Switch.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schemes", "Channel_Id", "dbo.Channels");
            DropForeignKey("dbo.Schemes", "Fees_Id", "dbo.Fees");
            DropForeignKey("dbo.Schemes", "TransType_Id", "dbo.TransactionTypes");
            DropIndex("dbo.Schemes", new[] { "Channel_Id" });
            DropIndex("dbo.Schemes", new[] { "Fees_Id" });
            DropIndex("dbo.Schemes", new[] { "TransType_Id" });
            AddColumn("dbo.SourceNodes", "NodeType", c => c.Int(nullable: false));
            AlterColumn("dbo.Schemes", "Channel_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Schemes", "Fees_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Schemes", "TransType_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Schemes", "Channel_Id");
            CreateIndex("dbo.Schemes", "Fees_Id");
            CreateIndex("dbo.Schemes", "TransType_Id");
            AddForeignKey("dbo.Schemes", "Channel_Id", "dbo.Channels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Schemes", "Fees_Id", "dbo.Fees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Schemes", "TransType_Id", "dbo.TransactionTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schemes", "TransType_Id", "dbo.TransactionTypes");
            DropForeignKey("dbo.Schemes", "Fees_Id", "dbo.Fees");
            DropForeignKey("dbo.Schemes", "Channel_Id", "dbo.Channels");
            DropIndex("dbo.Schemes", new[] { "TransType_Id" });
            DropIndex("dbo.Schemes", new[] { "Fees_Id" });
            DropIndex("dbo.Schemes", new[] { "Channel_Id" });
            AlterColumn("dbo.Schemes", "TransType_Id", c => c.Guid());
            AlterColumn("dbo.Schemes", "Fees_Id", c => c.Guid());
            AlterColumn("dbo.Schemes", "Channel_Id", c => c.Guid());
            DropColumn("dbo.SourceNodes", "NodeType");
            CreateIndex("dbo.Schemes", "TransType_Id");
            CreateIndex("dbo.Schemes", "Fees_Id");
            CreateIndex("dbo.Schemes", "Channel_Id");
            AddForeignKey("dbo.Schemes", "TransType_Id", "dbo.TransactionTypes", "Id");
            AddForeignKey("dbo.Schemes", "Fees_Id", "dbo.Fees", "Id");
            AddForeignKey("dbo.Schemes", "Channel_Id", "dbo.Channels", "Id");
        }
    }
}
