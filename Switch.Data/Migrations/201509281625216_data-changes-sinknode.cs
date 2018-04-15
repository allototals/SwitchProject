namespace Switch.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datachangessinknode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SinkNodes", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.SinkNodes", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.SinkNodes", "HostName", c => c.String(nullable: false));
            AlterColumn("dbo.SinkNodes", "IPAdress", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SinkNodes", "IPAdress", c => c.String());
            AlterColumn("dbo.SinkNodes", "HostName", c => c.String());
            AlterColumn("dbo.SinkNodes", "Name", c => c.String());
            DropColumn("dbo.SinkNodes", "IsDeleted");
        }
    }
}
