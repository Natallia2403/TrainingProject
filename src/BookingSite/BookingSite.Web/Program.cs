using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;

namespace BookingSite.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //try
            //{
            //    Log.Logger = SerilogConfiguration();
            //    Log.Information("BookingSite: LogAndSourceSuccesfullyAdded");
            //
                CreateWebHostBuilder(args).Build().Run();
            //}
            //catch (Exception ex)
            //{
            //    Log.Fatal(ex, "BookingSite: LogHostTerminated");
            //}
            //finally
            //{
            //    Log.CloseAndFlush();
            //}
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                                        .UseSerilog()
                                        .UseStartup<Startup>();

            return webHostBuilder;
        }

        /// <summary>
        /// Конфигурирование логирования (Serilog).
        /// </summary>
        /// <returns>Конфигурация.</returns>
        private static Logger SerilogConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                    .Build();

            var serilogConfig = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return serilogConfig;
        }
    }
}