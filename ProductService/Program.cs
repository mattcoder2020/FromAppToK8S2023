using Common.Logging;
using Common.Metrics;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace ProductService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
                 }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            //WebHost.CreateDefaultBuilder(args)
                 CostomWebHostBuilder(args)
                .UseStartup<Startup>()
                .UseLogging()
                .UseAppMetrics();

        public static IWebHostBuilder CostomWebHostBuilder(string[] args)
        {
            return Common.Web.Webhost.CreateDefaultBuilder(args);
        }
    }
}
