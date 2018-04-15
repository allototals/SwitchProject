namespace Switch.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datachanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransactionTypes", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.TransactionTypes", "Code", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.TransactionTypes", "Description", c => c.String(maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransactionTypes", "Description", c => c.String());
            AlterColumn("dbo.TransactionTypes", "Code", c => c.String());
            AlterColumn("dbo.TransactionTypes", "Name", c => c.String());
        }
    }
}
