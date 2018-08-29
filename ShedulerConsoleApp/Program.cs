using System;
using Hangfire;
using Hangfire.MemoryStorage;

namespace ShedulerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Sheduler.Core.DllSheduler dllSheduler = new Sheduler.Core.DllSheduler();
            GlobalConfiguration.Configuration
                //.UseColouredConsoleLogProvider()
                .UseMemoryStorage();

            //GlobalConfiguration.Configuration
            //    .UseColouredConsoleLogProvider()
            //    .UseSqlServerStorage(@"Data Source=DESKTOP-NJO5SNP\MSSQLSERVER2012;Initial Catalog=ShedulerDb;Persist Security Info=True;User ID=sa;Password=42025187;");

            RecurringJob.AddOrUpdate(() => Console.WriteLine("Do work..."),
             Cron.MinuteInterval(1));
            //RecurringJob.AddOrUpdate("hourly", () => Console.WriteLine("Hello"), "25 15 * * *");

            BackgroundJob.Enqueue(() => Console.WriteLine("Easy!"));

            using (new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press ENTER to exit...");
                Console.ReadLine();
            }

        }
    }
}
