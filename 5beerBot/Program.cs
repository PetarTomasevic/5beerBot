using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.Extensions.DependencyInjection;

namespace The5beerBot
{
    /// <summary>
    ///
    /// </summary>
    public class Program
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Setup and Start Kestrel
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
               .ConfigureLogging(x =>
             x.AddEventLog()
                 .AddDebug()
                 .AddEventLog()
             .AddConsole());

        //.ConfigureLogging(logging =>

        //{
        //    logging.ClearProviders();
        //    logging.AddAzureWebAppDiagnostics();
        //})
        //  .ConfigureServices(serviceCollection => serviceCollection
        //      .Configure<AzureFileLoggerOptions>(options =>
        //      {
        //          options.FileName = "azure-diagnostics-";
        //          options.FileSizeLimit = 10 * 1024;
        //          options.RetainedFileCountLimit = 5;
        //      })
        //  );

        //    .ConfigureLogging(x =>
        //x.AddEventLog()
        //    .AddDebug()
        //    .AddEventLog()
        //.AddConsole());
        //.AddFileLogging(Directory.GetCurrentDirectory() + "/App_Data/"));
    }
}