namespace LibraryGradProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoverImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "CoverImage", c => c.String(defaultValue: "https://upload.wikimedia.org/wikipedia/commons/8/87/Bucheinband.15.Jh.r.Inkunabel.jpg"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "CoverImage");
        }
    }
}
