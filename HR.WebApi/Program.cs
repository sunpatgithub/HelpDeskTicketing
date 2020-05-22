using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Reflection;
using System.Xml;

namespace HR.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //XmlDocument log4netConfig = new XmlDocument();
            //log4netConfig.Load(File.OpenRead("log4net.config"));
            //var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(),
            //           typeof(log4net.Repository.Hierarchy.Hierarchy));
            //log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
            //CreateWebHostBuilder(args).Build().Run();
            CreateWebHostBuilder(args).Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseUrls("http://*.8080")
        //        .UseStartup<Startup>()
        //        .UseKestrel(options =>
        //        {
        //            options.Limits.MaxRequestBodySize = 209715200;
        //        });

        public static IWebHost CreateWebHostBuilder(string[] args) =>
                        WebHost.CreateDefaultBuilder(args)
                        .UseUrls("http://*:8080")
                        .UseStartup<Startup>()
                        .UseKestrel(options =>
                        {
                            options.Limits.MaxRequestBodySize = 209715200;
                        })
                        .Build();
    }
}
