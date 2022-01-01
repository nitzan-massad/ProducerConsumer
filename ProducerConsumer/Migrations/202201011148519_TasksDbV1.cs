namespace ProducerConsumer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TasksDbV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.tasks",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        creationtime = c.DateTime(nullable: false),
                        modificationtime = c.DateTime(nullable: false),
                        tasktext = c.String(),
                        status = c.String(),
                        consumerid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("public.tasks");
        }
    }
}
