namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Edit2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TrainerClients", "Trainer_Id", "dbo.Trainers");
            DropForeignKey("dbo.TrainerClients", "Client_Id", "dbo.Clients");
            DropIndex("dbo.IndividualTrainings", new[] { "client_Id" });
            DropIndex("dbo.TrainerClients", new[] { "Trainer_Id" });
            DropIndex("dbo.TrainerClients", new[] { "Client_Id" });
            AddColumn("dbo.Clients", "Trainer_Id", c => c.Int());
            CreateIndex("dbo.Clients", "Trainer_Id");
            CreateIndex("dbo.IndividualTrainings", "Client_Id");
            AddForeignKey("dbo.Clients", "Trainer_Id", "dbo.Trainers", "Id");
            DropTable("dbo.TrainerClients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TrainerClients",
                c => new
                    {
                        Trainer_Id = c.Int(nullable: false),
                        Client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Trainer_Id, t.Client_Id });
            
            DropForeignKey("dbo.Clients", "Trainer_Id", "dbo.Trainers");
            DropIndex("dbo.IndividualTrainings", new[] { "Client_Id" });
            DropIndex("dbo.Clients", new[] { "Trainer_Id" });
            DropColumn("dbo.Clients", "Trainer_Id");
            CreateIndex("dbo.TrainerClients", "Client_Id");
            CreateIndex("dbo.TrainerClients", "Trainer_Id");
            CreateIndex("dbo.IndividualTrainings", "client_Id");
            AddForeignKey("dbo.TrainerClients", "Client_Id", "dbo.Clients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TrainerClients", "Trainer_Id", "dbo.Trainers", "Id", cascadeDelete: true);
        }
    }
}
