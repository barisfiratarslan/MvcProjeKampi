namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_contactadddate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "ContectDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "ContectDate");
        }
    }
}
