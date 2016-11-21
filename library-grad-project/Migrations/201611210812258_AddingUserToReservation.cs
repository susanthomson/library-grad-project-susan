namespace LibraryGradProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingUserToReservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Reservations", "UserId", c => c.Int(nullable: true));

            //Insert a default UserId
            Sql("Insert INTO dbo.Users (Name) VALUES ('Anonymous')");

            //Set all null Reservations.UserId to the default
            Sql("UPDATE dbo.Reservations SET UserId = 1 WHERE UserId IS NULL");

            AddForeignKey("dbo.Reservations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "UserId", "dbo.Users");
            DropColumn("dbo.Reservations", "UserId");
            DropTable("dbo.Users");
        }
    }
}
