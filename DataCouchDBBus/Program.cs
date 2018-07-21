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
            CouchDBConnect();

            var reportingThread = ReportingThread.GetInstance();
            reportingThread.StartInfoThread();
            var couch = new CouchDbRest();
            var res = couch.getCouchDbVer();
            int httpPort = IpPorts.TcpPort;
            url = string.Format("http://*:{0}", httpPort);
            CreateWebHostBuilder(args).Build().Run();
        }

        private static void CouchDBConnect()
        {

            var cred = new Credentials()
            {
                Username = "apiadmin",
                Password = "secret"
            };
            CouchDBClient couchClient = new CouchDBClient();
            couchClient.Auth(cred);
            Console.WriteLine("Auth cookie: {0}", couchClient.AuthCookie);
            couchClient.GetAllDataBases<string>();


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().UseUrls(url);
    }
}
