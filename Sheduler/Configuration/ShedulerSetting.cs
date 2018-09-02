using System.Collections.Generic;

namespace Sheduler.Core.Configuration
{
    public class ShedulerSetting
    {
        public bool AutoStart { get; set; }

        public List<WorkerSetting> Workers { get; set; }

        public int RestartAfterFailCount { get; set; }
    }
}
