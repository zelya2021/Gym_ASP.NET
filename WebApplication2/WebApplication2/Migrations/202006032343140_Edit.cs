namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainerClients",
                c => new
                    {
                        Trainer_Id = c.Int(nullable: false),
                        Client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trainer_Id, t.Client_Id })
                .ForeignKey("dbo.Trainers", t => t.Trainer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_Id, cascadeDelete: true)
                .Index(t => t.Trainer_Id)
                .Index(t => t.Client_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainerClients", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.TrainerClients", "Trainer_Id", "dbo.Trainers");
            DropIndex("dbo.TrainerClients", new[] { "Client_Id" });
            DropIndex("dbo.TrainerClients", new[] { "Trainer_Id" });
            DropTable("dbo.TrainerClients");
        }
    }
}
