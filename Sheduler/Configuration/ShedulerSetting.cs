using System;
using System.Collections.Generic;
using System.Text;

namespace Sheduler.Infrastructure.Settings
{
    public class ShedulerSetting
    {
        public bool AutoStart { get; set; }

        public List<string> Workers { get; set; }
    }
}
