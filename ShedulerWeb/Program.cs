using System.Runtime.InteropServices;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;


namespace ShedulerWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSetting("detailedErrors", "true")
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.Sources.Clear();
                    var env = context.HostingEnvironment;

                    var systemType = "windows";

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                        RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                        systemType = "unix";
                    
                    builder
                        .SetBasePath(env.ContentRootPath)
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                        .AddJsonFile($"appsettings.{systemType}.json", true);

                })
                .CaptureStartupErrors(true)
                .UseStartup<Startup>();
    }
}
