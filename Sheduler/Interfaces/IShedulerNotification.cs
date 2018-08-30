using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sheduler.Core.Interfaces
{
    public interface IShedulerNotification
    {
        Task SendNotificationAsync(string message);
    }
}
