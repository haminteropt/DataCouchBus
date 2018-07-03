using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DataCouchDBBus.CouchDB
{
    public class CouchDbRest
    {
        static HttpClient client = new HttpClient();
        public async Task<CouchDbVersion> getCouchDbVer()
        {
            try
            {
                var uri = new Uri("http://localhost:5984/");
                var result = await client.GetStringAsync(uri);
                var ver = JsonConvert.DeserializeObject<CouchDbVersion>(result);
                return ver;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
