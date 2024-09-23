using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace OgmentoAPI.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

        #if DEBUG
            configurationBuilder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
        #else
            configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        #endif

            var configuration = configurationBuilder.Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            try
            {
                Log.Information("Application starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "The application Failed to start correctly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
