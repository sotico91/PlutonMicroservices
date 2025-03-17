namespace MSPerson.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            // Eliminar la base de datos y sus tablas
            Sql("DROP TABLE IF EXISTS dbo.People");

            // Crear la tabla People
            CreateTable(
                "dbo.People",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    DocumentType = c.String(),
                    DocumentNumber = c.String(),
                    DateOfBirth = c.DateTime(nullable: false),
                    PhoneNumber = c.String(),
                    Email = c.String(),
                    PersonType = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            // Agregar datos iniciales
            Sql("INSERT INTO dbo.People (Name, DocumentType, DocumentNumber, DateOfBirth, PhoneNumber, Email, PersonType) VALUES ('John Doe', '0', '123456789', '1980-01-01', '555-1234', 'john.doe@example.com', 0)"); // Doctor
            Sql("INSERT INTO dbo.People (Name, DocumentType, DocumentNumber, DateOfBirth, PhoneNumber, Email, PersonType) VALUES ('Jane Smith', '1', '987654321', '1990-02-02', '555-5678', 'jane.smith@example.com', 1)"); // Patient
            Sql("INSERT INTO dbo.People (Name, DocumentType, DocumentNumber, DateOfBirth, PhoneNumber, Email, PersonType) VALUES ('Alice Johnson', '0', '123123123', '1985-03-03', '555-8765', 'alice.johnson@example.com', 0)"); // Doctor
            Sql("INSERT INTO dbo.People (Name, DocumentType, DocumentNumber, DateOfBirth, PhoneNumber, Email, PersonType) VALUES ('Bob Brown', '1', '321321321', '1975-04-04', '555-4321', 'bob.brown@example.com', 1)"); // Patient
            Sql("INSERT INTO dbo.People (Name, DocumentType, DocumentNumber, DateOfBirth, PhoneNumber, Email, PersonType) VALUES ('Charlie Davis', '1', '456456456', '1982-05-05', '555-6789', 'charlie.davis@example.com', 0)"); // Doctor
            Sql("INSERT INTO dbo.People (Name, DocumentType, DocumentNumber, DateOfBirth, PhoneNumber, Email, PersonType) VALUES ('Diana Evans', '0', '654654654', '1992-06-06', '555-9876', 'diana.evans@example.com', 1)"); // Patient
            Sql("INSERT INTO dbo.People (Name, DocumentType, DocumentNumber, DateOfBirth, PhoneNumber, Email, PersonType) VALUES ('Eve Foster', '1', '789789789', '1988-07-07', '555-3456', 'eve.foster@example.com', 0)"); // Doctor
            Sql("INSERT INTO dbo.People (Name, DocumentType, DocumentNumber, DateOfBirth, PhoneNumber, Email, PersonType) VALUES ('Frank Green', '0', '987987987', '1978-08-08', '555-6543', 'frank.green@example.com', 1)"); // Patient
            Sql("INSERT INTO dbo.People (Name, DocumentType, DocumentNumber, DateOfBirth, PhoneNumber, Email, PersonType) VALUES ('Grace Harris', '1', '111222333', '1983-09-09', '555-7890', 'grace.harris@example.com', 0)"); // Doctor
            Sql("INSERT INTO dbo.People (Name, DocumentType, DocumentNumber, DateOfBirth, PhoneNumber, Email, PersonType) VALUES ('Henry Irving', '0', '333222111', '1993-10-10', '555-0987', 'henry.irving@example.com', 1)"); // Patient
        }

        public override void Down()
        {
            // Eliminar la tabla People
            DropTable("dbo.People");
        }
    }
}
