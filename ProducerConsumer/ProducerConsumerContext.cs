using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class ProducerConsumerContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public ProducerConsumerContext():base(nameOrConnectionString: "ProducerConsumerDBConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
