using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCouchDBBus.CouchDB
{
    public class Vendor
    {
        public string name { get; set; }
    }
    public class CouchDbVersion
    {
        public string couchdb { get; set; }
        public string version { get; set; }
        public string[] features { get; set; }
        public Vendor vendor { get; set; }
    }
}
