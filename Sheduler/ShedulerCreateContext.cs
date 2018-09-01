using  Sheduler.Core.Configuration;
using Sheduler.Core.Interfaces;

namespace Sheduler.Core
{
    public class ShedulerCreateContext
    {
        public ShedulerSetting Settings { get; set; }
        public IDllWorker Worker { get; set; }
    }
}