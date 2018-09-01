using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Sheduler.Core.Configuration;
using Sheduler.Worker.Abstraction;

namespace Sheduler.Core.Interfaces
{
    public interface IDllWorker
    {
        IWorker Worker { get; set; }
        WorkerSetting Settings { get; set; }
        IConfiguration Configuration { get; set; }
        Guid WorkerId { get; set; }
    }
}
