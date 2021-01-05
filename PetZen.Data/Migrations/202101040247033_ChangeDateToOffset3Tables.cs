namespace PetZen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDateToOffset3Tables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Administrations", "AdminDateTime", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Activities", "Date", c => c.DateTimeOffset(nullable: false, precision: 7));
            AlterColumn("dbo.Feedings", "FeedDateTime", c => c.DateTimeOffset(nullable: false, precision: 7));
            DropColumn("dbo.Administrations", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Administrations", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Feedings", "FeedDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Activities", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Administrations", "AdminDateTime");
        }
    }
}
