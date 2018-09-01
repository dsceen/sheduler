using Microsoft.Extensions.Configuration;
using Sheduler.Core.Configuration;
using Sheduler.Core.Interfaces;
using Sheduler.Worker.Abstraction;
using System;

namespace Sheduler.Core
{
    public class DllWorker : IDllWorker
    {
        public IWorker Worker { get; set; }
        public WorkerSetting Settings { get; set; }
        public IConfiguration Configuration { get; set; }
        public Guid WorkerId { get; set; } = Guid.NewGuid();
    }
}
