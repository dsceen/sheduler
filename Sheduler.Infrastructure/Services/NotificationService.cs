using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using  Sheduler.Core.Interfaces;

namespace Sheduler.Infrastructure.Services
{
    public class NotificationService : IShedulerNotification
    {
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(ILogger<NotificationService> logger)
        {
            _logger = logger;
        }

        public Task SendNotificationAsync(string message)
        {
            _logger.LogInformation($"Message recived {message}");
            _logger.LogError("Not implemented");
            return Task.FromResult(0);
        }
    }
}
