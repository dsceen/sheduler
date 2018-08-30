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
        private readonly IShedulerNotification _shedulerNotification;
        private bool _status;

        private static readonly object StatusLocker = new object();

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
                if (!_status)
                {
                    _logger.LogInformation("Started");
                    _shedulerNotification.SendNotificationAsync("Sheduler started");
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
