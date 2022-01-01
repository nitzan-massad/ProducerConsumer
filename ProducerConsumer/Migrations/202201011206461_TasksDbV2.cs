namespace ProducerConsumer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TasksDbV2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("public.tasks");
            AlterColumn("public.tasks", "id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("public.tasks", "id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("public.tasks");
            AlterColumn("public.tasks", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("public.tasks", "id");
        }
    }
}
