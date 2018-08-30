﻿using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using Sheduler.Core.Interfaces;
using Sheduler.Core;
using Microsoft.Extensions.Options;
using Sheduler.Core.Configuration;

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