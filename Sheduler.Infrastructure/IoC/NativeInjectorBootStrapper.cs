﻿using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using Sheduler.Core.Interfaces;
using Sheduler.Core;

namespace Sheduler.Infrastructure.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            
            // Sheduler
            services.AddSingleton<IDllSheduler, DllSheduler>();

            // Hangfire library for background jobs
            services.AddHangfire(x => x.UseMemoryStorage());

        }
    }
}
