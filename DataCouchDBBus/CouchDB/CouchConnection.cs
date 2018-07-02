using HamBusLib.DataBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCouchDBBus.CouchDB
{
    public class CouchConnection
    {
        public static CouchConnection Instance = new CouchConnection();
        public DataConnection CouchConn { get; set; }
        private CouchConnection() { }
        public bool IsLoggedIn { get; set; } = false;
    }
}
