﻿using AdSite.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.IO;


namespace AdSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("Configs/nlog.config").GetCurrentClassLogger();
            logger.Info("app init");
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();

                    SeedExtension.MigrateDatabase(serviceProvider);
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

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var serviceProvider = services.GetRequiredService<IServiceProvider>();
                    var configuration = services.GetRequiredService<IConfiguration>();

                    SeedExtension.CreateRoles(serviceProvider).Wait();
                    SeedExtension.CreateDefaultCountry(serviceProvider, configuration).Wait();
                    SeedExtension.CreateDefaultLanguage(serviceProvider, configuration).Wait();
                    SeedExtension.ImportLocalizations(serviceProvider, configuration).Wait();
                    SeedExtension.CreateAdminAccount(serviceProvider).Wait();
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

        public static IHost BuildWebHost(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    // Set properties and call methods on options
                })
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddUserSecrets<Program>();

                    config.AddJsonFile("appsettings.json", optional: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

                    config.AddEnvironmentVariables();
                })
                .UseIISIntegration()
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Warning);
                })
                .UseNLog();// NLog: setup NLog for Dependency injection

            }).Build();
    }
}
