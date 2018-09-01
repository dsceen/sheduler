using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sheduler.Worker.Abstraction;

namespace SimpleWorker
{
    public class SimpleWorker : IWorker
    {
        private readonly ServiceProvider _provider;
        public SimpleWorker()
        {
            var services = new ServiceCollection();
            //services.AddSingleton<MyParam>();
            
            _provider = services.BuildServiceProvider();
            Services = services;
        }

        public IServiceCollection Services { get; }
        public Task<bool> StartAsync()
        {
            return Task.Run(() =>
            {
                Task.Delay(5000);
                return true;
            });
        }
    }
}
