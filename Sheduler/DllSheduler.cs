using System;
using System.Collections.Generic;
using System.IO;
using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sheduler.Core.Configuration;
using Sheduler.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Sheduler.Core.System;
using Sheduler.Worker.Abstraction;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sheduler.Core
{
    public class DllSheduler : IDllSheduler
    {
        private readonly ShedulerSetting _shedulerSetting;
        private readonly ILogger _logger;
        private readonly IShedulerNotification _shedulerNotification;

        private static readonly object StatusLocker = new object();
        private static readonly object InitializeWorkersLocker = new object();

        public bool IsStarted { get; private set; }
        public List<IDllWorker> Workers { get; } = new List<IDllWorker>();

        public DllSheduler(ShedulerSetting shedulerSetting, ILogger<DllSheduler> logger, IShedulerNotification shedulerNotification)
        {
            _logger = logger;
            _shedulerNotification = shedulerNotification;
            _shedulerSetting = shedulerSetting;
        }

        public void Start()
        {
            lock (StatusLocker)
            {
                if (!IsStarted)
                {
                    InitializeWorkers();
                    StartWorkers();
                    //ToDo: do work
                    //RecurringJob.AddOrUpdate(() => Console.WriteLine("Do work every minute " + DateTime.Now),
                    //Cron.MinuteInterval(1));
                    _logger.LogInformation("Started");
                    _shedulerNotification.SendNotificationAsync("Sheduler started");
                    IsStarted = true;
                }
            }
        }

        public void Stop()
        {
            lock (StatusLocker)
            {
                if (IsStarted)
                {
                    //ToDo: do work
                    _logger.LogInformation("Stoped");
                    _shedulerNotification.SendNotificationAsync("Sheduler stoped");
                    IsStarted = false;
                }
            }
        }

        private void InitializeWorkers()
        {
            lock (InitializeWorkersLocker)
            {
                Workers.Clear();
                foreach (var worker in _shedulerSetting.Workers)
                {
                    try
                    {
                        var fileInfo = new FileInfo(worker.PathToDll);
                        var directoryInfo = fileInfo.Directory;
                        if (!fileInfo.Exists || directoryInfo == null)
                        {
                            _logger.LogWarning("Dll Not found {0}", worker.PathToDll);
                            continue;
                        }

                        var asemblyLoader = new AssemblyLoader(directoryInfo.FullName);
                        var asm = asemblyLoader.LoadFromAssemblyPath(worker.PathToDll);
                        var type = asm.GetType("Worker.Worker");

                        IDllWorker initWorker = new DllWorker
                        {
                            Worker = (IWorker)Activator.CreateInstance(type),
                            Settings = worker,
                            Configuration = new ConfigurationBuilder().AddJsonFile(worker.PathToConfig).Build()
                        };

                        Workers.Add(initWorker);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, $"Worker initialization problem: {worker.PathToDll}");
                    }
                }
            }
        }

        private void StartWorkers()
        {
            foreach (var worker in Workers)
            {
                RecurringJob.AddOrUpdate<IDllSheduler>(worker.WorkerId.ToString(), sheduler => sheduler.DoWorkerJob(worker.WorkerId), Cron.Minutely);
            }
        }

        public async Task DoWorkerJob(Guid workerId)
        {
            var dllWorker = Workers.FirstOrDefault(x => x.WorkerId == workerId);
            if (dllWorker == null)
            {
                _logger.LogError("Worker no found: Id = {0}", workerId);
                throw new Exception($"Worker no found: Id = {workerId}");
            }
            var result = await dllWorker.Worker.StartAsync();
            _logger.LogInformation($"Work for worker {dllWorker.WorkerId} has executed with result: \r\n \"{result}\"");
        }
    }
}
