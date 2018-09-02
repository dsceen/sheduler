using Microsoft.Extensions.Configuration;
using Sheduler.Core.Interfaces;
using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Sheduler.Core.Extensions
{
    public static class WorkerExtension
    {
        /// <param name="lastCheckDelay">Last check delay in ms</param>
        public static void UpdateConfiguration(this IDllWorker worker,int lastCheckDelay)
        {
            if (!File.Exists(worker.Settings.PathToConfig)) return;
            if (DateTime.Now > File.GetLastWriteTime(worker.Settings.PathToConfig).AddMilliseconds(lastCheckDelay)) return;

            worker.Configuration = new ConfigurationBuilder().AddJsonFile(worker.Settings.PathToConfig).Build();
            worker.Worker.Services.Remove<IConfiguration>();
            worker.Worker.Services.AddSingleton(worker.Configuration);
        }
    }
}
