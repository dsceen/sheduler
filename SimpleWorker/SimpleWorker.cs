using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sheduler.Worker.Abstraction;

namespace Worker
{
    public class Worker : IWorker
    {
        private const string WorkerName = "SimpleWorker";

        public Worker()
        {

        }

        public IServiceCollection Services { get; } = new ServiceCollection();
        public Task<string> StartAsync()
        {
            return Task.Run(() =>
            {
                Task.Delay(5000);
                return $"{WorkerName} completed a job. {DateTime.Now}. Target URL Config: { GetConfiguration()["targetUrl"] }";
            });
        }

        private IConfiguration GetConfiguration() =>
            (IConfiguration) Services.BuildServiceProvider().GetService(typeof(IConfiguration));
    }
}
