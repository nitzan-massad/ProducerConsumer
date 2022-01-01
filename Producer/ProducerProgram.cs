using ProducerConsumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Producer
{
    internal class ProducerProgram
    {

        static void Main(string[] args)
        {
            bool addManually = GetIfUserWantToAddManually();
            if (addManually)
            {
                while (true) { AddTasksManuallyToDb(); }
            }
            else
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 10000000);
                while (true)
                {       
                    randomNumber++;
                    CrudOperation.AddTaskToDB("Hi Hi text here number "+ randomNumber);
                    //Thread.Sleep(2000);
                }
            }
        }

        private static bool GetIfUserWantToAddManually()
        {
            Console.WriteLine("Would you like to add Tasks manually? (y/n)");
            string userInput = Console.ReadLine();
            if (userInput != "y" && userInput != "n")
            {
                Console.WriteLine("invalid selection");
                return GetIfUserWantToAddManually();
            }
            return userInput == "y";
        }

        private static void AddTasksManuallyToDb()
        {
            Console.WriteLine("Insert Your Task Text:");
            string userInput = Console.ReadLine();
            CrudOperation.AddTaskToDB(userInput);
        }

    }
}
