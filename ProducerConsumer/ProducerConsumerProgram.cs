using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    internal class ProducerConsumerProgram
    {
        static void Main(string[] args)
        {
            bool isUserWantTasksByConsumer = GetIfUserWantTasksByConsumer();
            if (isUserWantTasksByConsumer)
            {
                PrintToScreenTasksByConsumer();
            }
            else
            {
                PrintToScreenStatistics();

            }
            Console.ReadLine();

        }

        private static void PrintToScreenStatistics()
        {
            int numberInStatusPending = CrudOperation.GetCountTaskByStatus(TaskStatus.Pending.ToString());
            int numberInStatusProgress = CrudOperation.GetCountTaskByStatus(TaskStatus.InProgress.ToString());
            int numberInStatusDone = CrudOperation.GetCountTaskByStatus(TaskStatus.Done.ToString());
            int numberInStatusError = CrudOperation.GetCountTaskByStatus(TaskStatus.Error.ToString());

            double avgOfDoneTaskTime = CrudOperation.CalculateAvgOfDoneTaskTime();
            double percentageOfErrors = CrudOperation.CalculatePercentageOfErrors();

            Console.WriteLine(String.Format("Tasks in Pending: {0}, In Progress: {1}, Done: {2}, Error: {3}", numberInStatusProgress, numberInStatusPending, numberInStatusDone, numberInStatusError));
            Console.WriteLine(String.Format("Avg processing time of successfully executed tasks: {0} seconds", avgOfDoneTaskTime));
            Console.WriteLine(String.Format("% of errors: {0}", percentageOfErrors));
        }

        private static void PrintToScreenTasksByConsumer()
        {
            List<int> consumersNumbers = GetConsumersNumbers();
            foreach (int consumerNumber in consumersNumbers)
            {
                List<Task> tasksByConsumer = CrudOperation.GetTasksByConsumerNumber(consumerNumber);
                Console.WriteLine(String.Format("Last tasks of consumer {0}", consumerNumber));
                foreach (Task task in tasksByConsumer)
                {
                    Console.WriteLine(String.Format("{0} {1} {2} {3}", task.Id, task.Status, task.ModificationTime, task.TaskText));
                }
            }
        }

        private static bool GetIfUserWantTasksByConsumer()
        {
            Console.WriteLine("Select your preference (1-2) \n" +
                "1. Get latest tasks foreach customer by CustomerID \n" +
                "2. Get statistics"); 
            string userInput = Console.ReadLine();
            if (userInput != "1" && userInput != "2")
            {
                Console.WriteLine("invalid selection");
                return GetIfUserWantTasksByConsumer();
            }
            return userInput == "1";
        }

        private static List<int> GetConsumersNumbers()
        {
            Console.WriteLine("Insert consumers numbers separated by sapce");
            string userInput = Console.ReadLine();
            List<int> ans = new List<int>();
            List<string> consumersNumbersStrings = userInput.Split(' ').ToList();
            foreach (string consumerNumber in consumersNumbersStrings)
            {
                try
                {
                    int result = Int32.Parse(consumerNumber);
                    ans.Add(result);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse '{userInput}' please try again");
                    return GetConsumersNumbers();
                }
            }          
            return ans;
         }
    }
}
