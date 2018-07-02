using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataCouchDBBus.CouchDB;
using HamBusLib;
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
            var couch = new CouchDbRest();
            var res = couch.getCouchDbVer();
            int httpPort = IpPorts.TcpPort;
            url = string.Format("http://*:{0}", httpPort);
            Console.WriteLine("http port: {0}", url);
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseUrls(url);
    }
}
