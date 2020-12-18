using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SingletonProject
{
    public class FileHandler
    {
        /// <summary>
        /// This field will give the instance of this class
        /// </summary>
        public static FileHandler GetInstance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// This field gives the printing task. With this task you can check 
        /// whether this task is running or not.
        /// </summary>
        public Task PrintingTask
        {
            get
            {
                return printingTask;
            }
        }

        /// <summary>
        /// This method add the given string to the queue
        /// If the printing task doesn't run it will start it otherwise doesn't do anything else.
        /// </summary>
        /// <param name="messageIn">The text that the caller wants to log to the log file</param>
        public void Print(string messageIn)
        {
            messages.Enqueue(messageIn);
            lock (lockObject)
            {
                if (isPrinting) return;

                isPrinting = true;
                printingTask = Task.Run(() => PrintMessage());
            }
        }

        private static readonly FileHandler instance = new FileHandler();
        private string currentPath;
        private string folderPathToWriteReports;
        private bool isPrinting = false;
        private object lockObject = new object();
        private ConcurrentQueue<string> messages = new ConcurrentQueue<string>();
        private Task printingTask;
        private ReaderWriterLockSlim readWriteLock = new ReaderWriterLockSlim();

        /// <summary>
        /// The constructor is private and this is a little bit wierd, I know.
        /// It means no one can instantiate this class
        /// </summary>
        private FileHandler()
        {
            currentPath = Directory.GetParent(typeof(Program).Assembly.Location).FullName;
            folderPathToWriteReports = currentPath + @"..\..\..\..\..\Logs";
        }

        /// <summary>
        /// Writes the text from the queue
        /// </summary>
        private void PrintMessage()
        {
            while (messages.Count > 0)
            {
                string filePath = folderPathToWriteReports + @"\log.txt";

                string message;
                while (!messages.TryDequeue(out message));

                WriteToFileThreadSafe(message + Environment.NewLine,
                    filePath);
            }

            lock (lockObject)
            {
                isPrinting = false;
            }
        }

        /// <summary>
        /// Writes the given text into the file which is at the given path.
        /// Does it in a threadsafe way.
        /// </summary>
        /// <param name="text">The text that the caller wants to write into the file</param>
        /// <param name="path">the path where the file is</param>
        private void WriteToFileThreadSafe(string text, string path)
        {
            // Set Status to Locked
            readWriteLock.EnterWriteLock();
            try
            {
                // Append text to the file
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(text);
                    sw.Close();
                }
            }
            finally
            {
                // Release lock
                readWriteLock.ExitWriteLock();
            }
        }
    }
}
