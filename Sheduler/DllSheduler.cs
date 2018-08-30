using System;
using Hangfire;
using Sheduler.Core.Interfaces;
using Sheduler.Infrastructure.Settings;

namespace Sheduler.Core
{
    public class DllSheduler : IDllSheduler
    {
        private readonly ShedulerSetting _shedulerSetting;

        public DllSheduler(ShedulerSetting shedulerSetting)
        {
            _shedulerSetting = shedulerSetting;
        }

        public void Start()
        {
            DllShedulerServer.Instance.Start();
            //RecurringJob.AddOrUpdate(() => Console.WriteLine("Do work every minute " + DateTime.Now),
            //    Cron.MinuteInterval(1));
        }

        public void Stop()
        {
            DllShedulerServer.Instance.Stop();
        }
    }
}
