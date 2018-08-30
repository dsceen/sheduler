using System;
using Hangfire;
using Sheduler.Core.Configuration;
using Sheduler.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Sheduler.Core
{
    public class DllSheduler : IDllSheduler
    {
        private readonly ShedulerSetting _shedulerSetting;
        private readonly ILogger _logger;
        private bool _status;

        private static readonly object StatusLocker = new object();

        public DllSheduler(ShedulerSetting shedulerSetting, ILogger<DllSheduler> logger)
        {
            _logger = logger;
            _shedulerSetting = shedulerSetting;
        }

        public void Start()
        {
            lock (StatusLocker)
            {
                if (!_status)
                {
                    _logger.LogInformation("Started");
                    //ToDo: do work
                    //RecurringJob.AddOrUpdate(() => Console.WriteLine("Do work every minute " + DateTime.Now),
                    //Cron.MinuteInterval(1));
                    _status = true;
                }
            }
        }

        public void Stop()
        {
            lock (StatusLocker)
            {
                if (_status)
                {
                    _logger.LogInformation("Stoped");
                    //ToDo: do work
                    _status = false;
                }
            }
        }
    }
}
