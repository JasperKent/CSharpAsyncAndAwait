using System;
using System.IO;
using System.Net;
using System.Threading;

namespace CSharpAsyncAndAwait
{
    class Program
    {
        private static void ReportThread()
        {
            Console.WriteLine($"Thread Id: {Thread.CurrentThread.ManagedThreadId}" );
        }

        private static void Blocking()
        {
            WebClient client = new WebClient();

            Console.WriteLine("Starting call...");

            ReportThread();

            var stream = client.OpenRead(new Uri("https://bbc.co.uk"));

            ReportThread();

            Console.WriteLine("Results in...");

            Console.WriteLine(new StreamReader(stream).ReadLine());
        }

        private static void WithCallback()
        {
            WebClient client = new WebClient();

            client.OpenReadCompleted += Client_OpenReadCompleted;

            Console.WriteLine("Starting call...");

            string message = "my message";

            ReportThread();

            client.OpenReadAsync(new Uri("https://bbc.co.uk"),message);

            Console.WriteLine("Doing something in the meantime...");
        }

        private static void Client_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            Console.WriteLine("Results in...");

            ReportThread();

            Console.WriteLine(e.UserState);

            Console.WriteLine(new StreamReader(e.Result).ReadLine());
        }

        private static async void WithAsync()
        {
            WebClient client = new WebClient();

            Console.WriteLine("Starting call...");

            ReportThread();

            var stream = await client.OpenReadTaskAsync(new Uri("https://bbc.co.uk"));

            ReportThread();

            Console.WriteLine("Results in...");

            Console.WriteLine(new StreamReader(stream).ReadLine());
        }

        static void Main(string[] args)
        {
            //Blocking();
            //Console.WriteLine();

            //WithCallback();
            //Thread.Sleep(2000);
            //Console.WriteLine();

            WithAsync();
            Console.WriteLine("Doing something in the meantime...");
            Thread.Sleep(2000);
            Console.WriteLine();
        }
    }
}
