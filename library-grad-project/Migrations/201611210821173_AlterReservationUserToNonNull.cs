namespace LibraryGradProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterReservationUserToNonNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservations", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Reservations", new[] { "UserId" });
            AlterColumn("dbo.Reservations", "UserId", c => c.Int(nullable: true));
        }
    }
}
