using HamBusLib;
using HamBusLib.UdpNetwork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataCouchDBBus
{
    public class ReportingThread
    {
        private UdpClient udpClient = new UdpClient();
        private static ReportingThread Instance = null;
        private Thread infoThread;

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DataBusInfo dataBusDesc = DataBusInfo.Instance;
        public static ReportingThread GetInstance()
        {
            if (Instance == null)
                Instance = new ReportingThread();

            return Instance;
        }
        private ReportingThread()
        {

        }

        public void StartInfoThread()
        {
            var udpServer = UdpServer.GetInstance();
            dataBusDesc = DataBusInfo.Instance;
            var hostName = Dns.GetHostName(); // Retrive the Name of HOST  

            dataBusDesc.Command = "update";
            dataBusDesc.DocType = "DataBus";
            dataBusDesc.Id = Id;
            dataBusDesc.UdpPort = udpServer.listenUdpPort;
            dataBusDesc.TcpPort = udpServer.listenTcpPort;
            dataBusDesc.MinVersion = 1;
            dataBusDesc.MaxVersion = 1;
            dataBusDesc.Host = hostName;
            dataBusDesc.Name = "CouchDB DataBus";

            // Get the IP  
            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();

            infoThread = new Thread(SendRigBusInfo);
            infoThread.Start();
        }
        public void SendRigBusInfo()
        {
            while (true)
            {
                dataBusDesc.Time = DateTimeUtils.ConvertToUnixTime(DateTime.Now);
                udpClient.Connect("255.255.255.255", Constants.DirPortUdp);
                Byte[] senddata = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataBusDesc));
                udpClient.Send(senddata, senddata.Length);
                Thread.Sleep(3000);
            }
        }
    }
}

