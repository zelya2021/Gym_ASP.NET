namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConnection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupTrainings", "Client_Id", "dbo.Clients");
            DropIndex("dbo.GroupTrainings", new[] { "Client_Id" });
            DropIndex("dbo.IndividualTrainings", new[] { "Client_Id" });
            CreateTable(
                "dbo.GroupTrainingClients",
                c => new
                    {
                        GroupTraining_Id = c.Int(nullable: false),
                        Client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GroupTraining_Id, t.Client_Id })
                .ForeignKey("dbo.GroupTrainings", t => t.GroupTraining_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_Id, cascadeDelete: true)
                .Index(t => t.GroupTraining_Id)
                .Index(t => t.Client_Id);
            
            CreateIndex("dbo.IndividualTrainings", "client_Id");
            DropColumn("dbo.GroupTrainings", "Client_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupTrainings", "Client_Id", c => c.Int());
            DropForeignKey("dbo.GroupTrainingClients", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.GroupTrainingClients", "GroupTraining_Id", "dbo.GroupTrainings");
            DropIndex("dbo.GroupTrainingClients", new[] { "Client_Id" });
            DropIndex("dbo.GroupTrainingClients", new[] { "GroupTraining_Id" });
            DropIndex("dbo.IndividualTrainings", new[] { "client_Id" });
            DropTable("dbo.GroupTrainingClients");
            CreateIndex("dbo.IndividualTrainings", "Client_Id");
            CreateIndex("dbo.GroupTrainings", "Client_Id");
            AddForeignKey("dbo.GroupTrainings", "Client_Id", "dbo.Clients", "Id");
        }
    }
}
