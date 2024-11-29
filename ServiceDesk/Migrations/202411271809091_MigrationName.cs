namespace ServiceDesk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationName : DbMigration
    {
        public override void Up()
        {
            
            AlterColumn("dbo.Usuario", "Id", c => c.Int(nullable: false));
            
        }
        
        public override void Down()
        {
           
            AlterColumn("dbo.Usuario", "Id", c => c.Int(nullable: false, identity: true));
            
        }
    }
}
