using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    [Table("tasks")]
   public class Task
    {
        public Task(string taskText)
        {
            TaskText = taskText;
            Id = Guid.NewGuid().ToString();
            CreationTime = DateTime.UtcNow; 
            ModificationTime = DateTime.UtcNow;
            Status = TaskStatus.Pending.ToString();
        }
        public Task(){}

        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("creationtime")]
        public DateTime CreationTime { get; set; }

        [Column("modificationtime")]
        public DateTime ModificationTime { get; set; }

        [Column("tasktext")]
        public string TaskText { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("consumerid")]
        public int ConsumerID { get; set; }



    }

    enum TaskStatus
    {
        Pending,
        InProgress,
        Error,
        Done
    }

}
