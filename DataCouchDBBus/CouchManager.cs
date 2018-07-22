using CouchDB;
using CouchDB.Model;
using DataCouchDBBus.CouchDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCouchDBBus
{
    public class CouchManager
    {
        private CouchDBClient couchClient;
        public CouchDbVersion Version { get; set; }
        public List<string>  Databases { get; set; }
        public async void CouchDBConnect()
        {
            var cred = new Credentials()
            {
                Username = "apiadmin",
                Password = "secret"
            };
            couchClient = new CouchDBClient("http://localhost:5984");
            couchClient.Auth(cred);
            Databases = couchClient.GetAllDataBases<string>();
            Console.WriteLine("Auth cookie: {0}", couchClient.AuthCookie);

            var couch = new CouchDbRest();
            Version = await couch.getCouchDbVer();
            VerifyDb("hambusconfig");
        }
        public void VerifyDb(string dbName)
        {
            var db = Databases.Find(n =>
            {
                if (n == dbName)
                    return true;
                return false;
            });
            if (db == null)
                couchClient.CreateDataBase<string>(dbName);
        }
    }
}
