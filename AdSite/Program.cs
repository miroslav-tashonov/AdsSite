using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdSite.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;


namespace AdSite
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            logger.Info("app init");
            var host = BuildWebHost(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();

                    SeedRoles.CreateRoles(serviceProvider, configuration).Wait();

                }
                catch (Exception exception)
                {
                    logger.Error(exception, "An error occurred while creating roles");
                }
                finally
                {
                    NLog.LogManager.Shutdown();
                }
            }
            
            host.Run();

        }

        public static IWebHostBuilder BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Warning);
                })
                .UseNLog();  // NLog: setup NLog for Dependency injection
    }
}
