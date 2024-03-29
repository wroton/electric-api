using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Service.Server.Infrastructure;

namespace Service.Server
{
    /// <summary>
    /// Contains the entry point of the application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        /// <param name="arguments">Command-line arguments to use.</param>
        public static void Main(string[] arguments)
        {
            // Create a host builder using defaults.
            var hostBuilder = Host.CreateDefaultBuilder(arguments);
            hostBuilder.ConfigureWebHostDefaults(webHostBuilder =>
            {
                webHostBuilder.UseStartup<Startup>().ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole();
                    loggingBuilder.AddDebug();
                    loggingBuilder.AddAzureWebAppDiagnostics();
                });
            });

            // Build the host.
            var host = hostBuilder.Build();

            // Run the host. This blocks until it closes.
            host.Run();
        }
    }
}
