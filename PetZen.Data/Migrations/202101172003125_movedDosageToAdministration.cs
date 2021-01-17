namespace PetZen.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class movedDosageToAdministration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Administrations", "Dosage", c => c.Double(nullable: false));
            AddColumn("dbo.Administrations", "DoseMeasure", c => c.Int(nullable: false));
            DropColumn("dbo.Medications", "Dosage");
            DropColumn("dbo.Medications", "DoseMeasure");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Medications", "DoseMeasure", c => c.Int(nullable: false));
            AddColumn("dbo.Medications", "Dosage", c => c.Double(nullable: false));
            DropColumn("dbo.Administrations", "DoseMeasure");
            DropColumn("dbo.Administrations", "Dosage");
        }
    }
}
