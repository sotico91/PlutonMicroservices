namespace MSAuthServ.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            // Eliminar la base de datos y sus tablas
            Sql("DROP TABLE IF EXISTS dbo.Users");

            // Crear la tabla People
            CreateTable(
         "dbo.Users",
         c => new
         {
             Id = c.Int(nullable: false, identity: true),
             Username = c.String(),
             Password = c.String(),
         })
         .PrimaryKey(t => t.Id);

            Sql("INSERT INTO dbo.Users (Username, Password) VALUES ('admin', '123456')");

        }

        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
