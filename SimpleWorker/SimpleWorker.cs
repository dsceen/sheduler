using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sheduler.Worker.Abstraction;

namespace Worker
{
    public class Worker : IWorker
    {
        private const string WorkerName = "SimpleWorker";

        private readonly ServiceProvider _provider;
        public Worker()
        {
            var services = new ServiceCollection();
            //services.AddSingleton<MyParam>();
            
            _provider = services.BuildServiceProvider();
            Services = services;
        }

        public IServiceCollection Services { get; }
        public Task<string> StartAsync()
        {
            return Task.Run(() =>
            {
                Task.Delay(5000);
                return $"{WorkerName} completed a job. {DateTime.Now}";
            });
        }

        private string Target { get; set; }
    }
}
