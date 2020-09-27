namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupTrainings", "Time", c => c.String());
            AddColumn("dbo.IndividualTrainings", "Time", c => c.String());
            DropColumn("dbo.GroupTrainings", "StartTraining");
            DropColumn("dbo.GroupTrainings", "EndTraining");
            DropColumn("dbo.IndividualTrainings", "StartTraining");
            DropColumn("dbo.IndividualTrainings", "EndTraining");
        }
        
        public override void Down()
        {
            AddColumn("dbo.IndividualTrainings", "EndTraining", c => c.String());
            AddColumn("dbo.IndividualTrainings", "StartTraining", c => c.String());
            AddColumn("dbo.GroupTrainings", "EndTraining", c => c.String());
            AddColumn("dbo.GroupTrainings", "StartTraining", c => c.String());
            DropColumn("dbo.IndividualTrainings", "Time");
            DropColumn("dbo.GroupTrainings", "Time");
        }
    }
}
