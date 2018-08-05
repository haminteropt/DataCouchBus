using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CouchDB;
using CouchDB.Model;
using DataCouchDBBus.CouchDB;
using HamBusLib;
using HamBusLib.CouchDB;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace DataCouchDBBus
{
    public class Program
    {
        private static string url { get; set; }
        public static void Main(string[] args)
        {

            var manager = new CouchManager();
            manager.CouchDBConnect();

            var reportingThread = ReportingThread.GetInstance();
            reportingThread.StartInfoThread();

            int httpPort = IpPorts.TcpPort;
            url = string.Format("http://*:{0}", httpPort);
            CreateWebHostBuilder(args).Build().Run();
            Console.ReadKey();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(SetupConfiguration)
            .UseUrls(url)
            .UseStartup<Startup>();

        private static void SetupConfiguration(WebHostBuilderContext wbCtx, IConfigurationBuilder builder)
        {
            builder.Sources.Clear();
            builder.AddJsonFile("config.json", false, true)
                   .AddEnvironmentVariables();

        }

    }
}
