namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrainings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupTrainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTraining = c.String(),
                        EndTraining = c.String(),
                        Day = c.DateTime(nullable: false),
                        Trainer_Id = c.Int(),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trainers", t => t.Trainer_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Trainer_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.IndividualTrainings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTraining = c.String(),
                        EndTraining = c.String(),
                        Day = c.DateTime(nullable: false),
                        Trainer_Id = c.Int(),
                        Client_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trainers", t => t.Trainer_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Trainer_Id)
                .Index(t => t.Client_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IndividualTrainings", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.GroupTrainings", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.IndividualTrainings", "Trainer_Id", "dbo.Trainers");
            DropForeignKey("dbo.GroupTrainings", "Trainer_Id", "dbo.Trainers");
            DropIndex("dbo.IndividualTrainings", new[] { "Client_Id" });
            DropIndex("dbo.IndividualTrainings", new[] { "Trainer_Id" });
            DropIndex("dbo.GroupTrainings", new[] { "Client_Id" });
            DropIndex("dbo.GroupTrainings", new[] { "Trainer_Id" });
            DropTable("dbo.IndividualTrainings");
            DropTable("dbo.GroupTrainings");
        }
    }
}
