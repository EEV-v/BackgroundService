using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace BackgroundService
{
    public class Program
    {
        private static string AppHostingEnvironment =>
#if DEBUG
            "Development";
#else
            "Production";
#endif
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder
                .ConfigureNLog($"nlog.{AppHostingEnvironment}.config")
                .GetCurrentClassLogger();
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                logger.Fatal(ex.ToString());
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);

            hostBuilder.UseWindowsService();

            hostBuilder
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddEnvironmentVariables()
                        .AddCommandLine(args);
                });

            hostBuilder
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

            return hostBuilder;
        }
    }

}
