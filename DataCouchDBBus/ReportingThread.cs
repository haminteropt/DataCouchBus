using HamBusLib;
using HamBusLib.Models;
using HamBusLib.Packets;
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

        public DataBusInfo dataBusDesc = new DataBusInfo();
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
            var hostName = Dns.GetHostName(); // Retrive the Name of HOST  

            dataBusDesc.Command = "update";
            dataBusDesc.Id = Id;
            dataBusDesc.DocType = DocTypes.DataBusInfo;
            dataBusDesc.UdpPort = udpServer.listenUdpPort;
            dataBusDesc.TcpPort = udpServer.listenTcpPort;
            dataBusDesc.MinVersion = 1;
            dataBusDesc.MaxVersion = 1;
            dataBusDesc.Host = hostName;
            dataBusDesc.Name = "CouchDB DataBus";
            dataBusDesc.Description = "DataBus for CouchDB";

            // Get the IP  
            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();

            infoThread = new Thread(SendRigBusInfo);
            infoThread.Start();
        }
        public void SendRigBusInfo()
        {
            //https://stackoverflow.com/questions/2281441/can-i-set-the-timeout-for-udpclient-in-c
            int count = 0;
            var timeToWait = TimeSpan.FromSeconds(10);
            var ServerEp = new IPEndPoint(IPAddress.Any, 0);
            DirGreetingList dirList = DirGreetingList.Instance;
            udpClient.EnableBroadcast = true;
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
            while (true)
            {
                try
                {
                    dataBusDesc.Time = DateTimeUtils.ConvertToUnixTime(DateTime.Now);
                    Byte[] senddata = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataBusDesc));


                    udpClient.Send(senddata, senddata.Length, new IPEndPoint(IPAddress.Broadcast, 7300));
                    Console.WriteLine("Sending Data {0}",count++);
                    var ServerResponseData = udpClient.Receive(ref ServerEp);
                    var ServerResponse = Encoding.ASCII.GetString(ServerResponseData);
                    var dirService = DirectoryBusGreeting.ParseCommand(ServerResponse);
                    DirGreetingList.Instance.Add(dirService);
                    Thread.Sleep(3000);
                } catch(Exception e)
                {
                    Console.WriteLine("Exception: {0}", e.Message);
                    //infoThread.Abort();
                }
            }
        }
    }
}

