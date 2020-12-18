using System;
using System.Threading;
using System.Threading.Tasks;

namespace SingletonProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Task[] tasks = CreateLoggerTasks(5);

            StartTasks(tasks);
            
            Task.WaitAll(tasks);

            WaitingForTheEndOfLogging();

            Console.WriteLine("\nEvery message has been logged." +
                "\nNow you can close this application and check the content of the log file.");

            Console.ReadKey();
        }

        private static void StartTasks(Task[] tasks)
        {
            foreach (Task task in tasks)
            {
                task.Start();
            }
        }

        private static Task[] CreateLoggerTasks(int numberOfTasks)
        {
            Task[] tasks = new Task[numberOfTasks];
            for (int i = 0; i < numberOfTasks; i++)
            {
                string message = $"Log record of {i}. task";
                tasks[i] = new Task(() => Logging(message));
            }
            return tasks;
        }

        private static int WaitingForTheEndOfLogging()
        {
            int count = 0;
            do
            {
                if (count % 4 != 0)
                {
                    Console.Write(".");
                    count++;
                }
                else
                {
                    Console.Clear();
                    Console.Write("We are logging now");
                    count++;
                }
                Thread.Sleep(500);
            } while (FileHandler.GetInstance.PrintingTask.Status != TaskStatus.RanToCompletion);
            return count;
        }

        private static void Logging(string messageToLog)
        {
            for (int i = 0; i < 200; i++)
            {
                FileHandler.GetInstance.Print(messageToLog + $": numero {i}.");
            }
        }
    }
}
