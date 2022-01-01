using ProducerConsumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task = ProducerConsumer.Task;

namespace Consumer
{
    internal class ConsumerProgram
    {
        private static int bulkSize;
        private static string endAll;
        static void Main(string[] args)
        {
            int consumersNumber = GetNumberOfConsumers();
            bulkSize = GetBulkSize();

            for (int i = 0; i < consumersNumber; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ConsumerActive));
                //Thread.Sleep(2000);

            }
            endAll =Console.ReadLine();
        }

        public static void ConsumerActive(object obj)
        {
            while (endAll ==null)
            {
                List<Task> tasks = CrudOperation.GetInProgressTasksByBulk(bulkSize);
                foreach (Task task in tasks)
                {
                    string messageToPrintToScreen = string.Format("Consumer number: {0} print to screen the message: {1} ",task.ConsumerID, task.TaskText);
                    Console.WriteLine(messageToPrintToScreen);
                    CrudOperation.ChangeTaskToDone(task.Id);
                }
                //Thread.Sleep(6000);
            }
        }


        private static int GetNumberOfConsumers()
        {
            Console.WriteLine("How many Consumers would you like?");
            string userInput = Console.ReadLine();
            try
            {
                int result = Int32.Parse(userInput);
                if (result > 100)
                {
                    Console.WriteLine("Consumers number cant be bigger than 100");
                    return GetNumberOfConsumers();
                }
                return result;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{userInput}'");
                return GetNumberOfConsumers();
            }
        }

        private static int GetBulkSize()
        {
            Console.WriteLine("What is the Bulk size?");
            string userInput = Console.ReadLine();
            try
            {
                int result = Int32.Parse(userInput);
                if (result > 100)
                {
                    Console.WriteLine(" Bulk size cant be bigger than 100");
                    return GetBulkSize();
                }
                return result;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{userInput}'");
                return GetBulkSize();
            }
        }

    }
}
