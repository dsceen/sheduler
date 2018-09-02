using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sheduler.Core.Configuration;
using Sheduler.Core.Interfaces;
using Sheduler.Core.System;
using Sheduler.Worker.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Sheduler.Core.Extensions;

namespace Sheduler.Core
{
    public class DllSheduler : IDllSheduler
    {
        private const int ConfigurationObserverDelay = 4000;
        private readonly ShedulerSetting _shedulerSetting;
        private readonly ILogger _logger;
        private readonly IShedulerNotification _shedulerNotification;

        private static readonly object StatusLocker = new object();
        private static readonly object InitializeWorkersLocker = new object();

        private static readonly CancellationTokenSource CancelTokenSource = new CancellationTokenSource();
        private static CancellationToken _tokenForConfigurationObserver = CancelTokenSource.Token;

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
                if (IsStarted) return;
                InitializeWorkers();
                StartWorkers();
                StartConfigurationObserver();
                _logger.LogInformation("Started");
                _shedulerNotification.SendNotificationAsync("Sheduler started");
                IsStarted = true;
            }
        }

        public void Stop()
        {
            lock (StatusLocker)
            {
                if (!IsStarted) return;
                StopAllWorkers();
                CancelTokenSource.Cancel();
                _logger.LogInformation("Stoped");
                _shedulerNotification.SendNotificationAsync("Sheduler stoped");
                IsStarted = false;
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
                        //ToDo: Get assembly name from configuration file
                        var type = asm.GetType("Worker.Worker");

                        IDllWorker initWorker = new DllWorker
                        {
                            Worker = (IWorker)Activator.CreateInstance(type),
                            Settings = worker,
                            Configuration = new ConfigurationBuilder().AddJsonFile(worker.PathToConfig).Build()
                        };

                        initWorker.Worker.Services.AddSingleton(initWorker.Configuration);
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
                RecurringJob.AddOrUpdate<IDllSheduler>(worker.WorkerId.ToString(), sheduler => sheduler.DoWorkerJob(worker.WorkerId), worker.Settings.StartAt);
                
            }
        }

        private void StopAllWorkers()
        {
            foreach (var worker in Workers)
            {
                RecurringJob.RemoveIfExists(worker.WorkerId.ToString());
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
            _logger.LogInformation(
                $"Job for worker {dllWorker.WorkerId} has done with result: {Environment.NewLine} \"{result}\"");
        }

        private void StartConfigurationObserver()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (_tokenForConfigurationObserver.IsCancellationRequested)
                    {
                        _logger.LogDebug("Configuration observer stoped");
                        return;
                    }
                    Task.Delay(ConfigurationObserverDelay).Wait();
                    UpdateConfiguration();
                }
            }, _tokenForConfigurationObserver);
        }

        private void UpdateConfiguration()
        {
            foreach (var worker in Workers)
            {
                worker.UpdateConfiguration(ConfigurationObserverDelay);
            }
        }
    }
}
