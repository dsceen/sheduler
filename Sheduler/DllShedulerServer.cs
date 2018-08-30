using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sheduler.Core
{
    internal class DllShedulerServer
    {
        private bool _status;
        private static readonly object StatusLocker = new object();
        private static readonly Lazy<DllShedulerServer> Lazy =
            new Lazy<DllShedulerServer>(() => new DllShedulerServer());

        public static DllShedulerServer Instance => Lazy.Value;

        public void Start()
        {
            lock (StatusLocker)
            {
                Console.WriteLine(!_status ? "Server starting..." : "Server alredy started");
                if (!_status)
                {
                    //ToDo: do work
                    _status = true;
                }

            }
        }

        public void Stop()
        {
            lock (StatusLocker)
            {
                Console.WriteLine(_status ? "Server stoping..." : "Server not started");
                if (_status)
                {
                    //ToDo: do work
                    _status = false;
                }
            }
        }
    }
}
