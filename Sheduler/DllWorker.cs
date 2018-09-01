using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sheduler.Core.Configuration;
using Sheduler.Core.Interfaces;
using Sheduler.Worker.Abstraction;

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
