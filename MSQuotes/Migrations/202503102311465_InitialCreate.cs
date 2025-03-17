namespace MSQuotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
               "dbo.Quotes",
               c => new
               {
                   Id = c.Int(nullable: false, identity: true),
                   Date = c.DateTime(nullable: false),
                   Location = c.String(),
                   PatientId = c.Int(nullable: false),
                   DoctorId = c.Int(nullable: false),
                   Status = c.Int(nullable: false),
               })
               .PrimaryKey(t => t.Id);

            // Agregar datos iniciales
            Sql("INSERT INTO dbo.Quotes (Date, Location, PatientId, DoctorId, Status) VALUES ('2025-03-10', 'Clinic A', 1, 1, 0)"); // Pending
            Sql("INSERT INTO dbo.Quotes (Date, Location, PatientId, DoctorId, Status) VALUES ('2025-03-11', 'Clinic B', 2, 2, 1)"); // InProgress
            Sql("INSERT INTO dbo.Quotes (Date, Location, PatientId, DoctorId, Status) VALUES ('2025-03-12', 'Clinic C', 3, 3, 2)"); // Completed
            Sql("INSERT INTO dbo.Quotes (Date, Location, PatientId, DoctorId, Status) VALUES ('2025-03-13', 'Clinic D', 4, 4, 0)"); // Pending
            Sql("INSERT INTO dbo.Quotes (Date, Location, PatientId, DoctorId, Status) VALUES ('2025-03-14', 'Clinic E', 5, 5, 1)"); // InProgress
            Sql("INSERT INTO dbo.Quotes (Date, Location, PatientId, DoctorId, Status) VALUES ('2025-03-15', 'Clinic F', 6, 6, 2)"); // Completed
            Sql("INSERT INTO dbo.Quotes (Date, Location, PatientId, DoctorId, Status) VALUES ('2025-03-16', 'Clinic G', 7, 7, 0)"); // Pending
            Sql("INSERT INTO dbo.Quotes (Date, Location, PatientId, DoctorId, Status) VALUES ('2025-03-17', 'Clinic H', 8, 8, 1)"); // InProgress
            Sql("INSERT INTO dbo.Quotes (Date, Location, PatientId, DoctorId, Status) VALUES ('2025-03-18', 'Clinic I', 9, 9, 2)"); // Completed
            Sql("INSERT INTO dbo.Quotes (Date, Location, PatientId, DoctorId, Status) VALUES ('2025-03-19', 'Clinic J', 10, 10, 0)"); // Pending
        }

        public override void Down()
        {
            // Eliminar la tabla Quotes
            DropTable("dbo.Quotes");
        }
    }
}
