using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using Sheduler.Core.Interfaces;
using Sheduler.Core;
using Sheduler.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Sheduler.Infrastructure.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Configuration
            services.AddSingleton(x => x.GetRequiredService<IOptions<ShedulerSetting>>().Value);

            // Sheduler
            services.AddSingleton<IDllSheduler, DllSheduler>();

            // Hangfire library for background jobs
            services.AddHangfire(x => x.UseMemoryStorage());

        }
    }
}
