using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace App1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(Configuration)
                .UseUrls(Configuration.GetValue<string>("URL"))
                .UseStartup<Startup>();

        private static IConfiguration Configuration
        {
            get
            {
                ConfigurationBuilder builder = new ConfigurationBuilder();
                builder.AddJsonFile("./appsettings.json");
                builder.AddEnvironmentVariables();
                return builder.Build();
            }
        }
    }
}
