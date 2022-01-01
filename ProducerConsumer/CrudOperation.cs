using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public static class CrudOperation
    {

        //private static Mutex extractMutex = new Mutex();
        private static Mutex mutex = new Mutex();


        public static void AddTaskToDB (string taskText)
        {
            mutex.WaitOne();
            using (var dbContext = new ProducerConsumerContext())
            {
                dbContext.Tasks.Add(new Task(taskText));
                dbContext.SaveChanges();
            }
            mutex.ReleaseMutex();
        }

        public static List<Task> GetInProgressTasksByBulk(int bulkSize)
        {
            mutex.WaitOne();
            using (var dbContext = new ProducerConsumerContext())
            {
              
                List<Task> tasks = dbContext.Tasks.ToList().Where(task => task.Status == TaskStatus.Pending.ToString()).Take(bulkSize).ToList();
                foreach (var task in tasks) { 
                    task.Status = TaskStatus.InProgress.ToString();
                    task.ModificationTime = DateTime.UtcNow;
                    task.ConsumerID = Thread.CurrentThread.ManagedThreadId;
                }
                dbContext.SaveChanges();
                mutex.ReleaseMutex();
                return tasks.ToList();
            }

        } 

        public static void ChangeTaskToDone(string taskId)
        {
            mutex.WaitOne();
            using (var dbContext = new ProducerConsumerContext())
            {
                Task taskToDone = (Task)dbContext.Tasks.FirstOrDefault(task => task.Id == taskId);
                if (taskToDone != null)
                {
                    taskToDone.Status = TaskStatus.Done.ToString();
                    taskToDone.ModificationTime = DateTime.UtcNow;
                    dbContext.SaveChanges();
                }
            }
            mutex.ReleaseMutex();
        }

        public static List<Task> GetTasksByConsumerNumber(int consumer)
        {
            using (var dbContext = new ProducerConsumerContext())
            {
                List<Task> tasks = dbContext.Tasks.ToList().Where(task => task.ConsumerID == consumer).OrderByDescending(task => task.ModificationTime).Take(10).ToList();
                return tasks.ToList();
            }
        }

        public static int GetCountTaskByStatus(string status)
        {
            using (var dbContext = new ProducerConsumerContext())
            {
                int numberInStatus = dbContext.Tasks.ToList().Where(task => task.Status == status).Count();
                return numberInStatus;
            }
        }

        public static double CalculateAvgOfDoneTaskTime()
        {
            using (var dbContext = new ProducerConsumerContext())
            {
                float sumOfFinishingAllTasks = 0;
                List<Task> tasks = dbContext.Tasks.ToList().Where(task => task.Status == TaskStatus.Done.ToString()).ToList();
                foreach (var task in tasks)
                {
                    TimeSpan duration =  task.ModificationTime- task.CreationTime ;
                    sumOfFinishingAllTasks += ((float)duration.TotalSeconds);
                }
                return sumOfFinishingAllTasks /tasks.Count;
            }
        }

        public static double CalculatePercentageOfErrors()
        {
            using (var dbContext = new ProducerConsumerContext())
            {
                int numberOfErrors = GetCountTaskByStatus(TaskStatus.Error.ToString());
                int numberOfTasks = dbContext.Tasks.ToList().Count();
                return numberOfErrors / numberOfTasks;
            }
        }

    }
}
