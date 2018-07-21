using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCouchDBBus.CouchDB;
using HamBusLib.CouchDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataCouchDBBus.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public CouchDbRest couchClient { get; set; } = new CouchDbRest();
        [HttpPost]
        public async Task<CouchDbVersion> Post([FromBody] DataConnection value)
        {
            var couchConn = CouchConnection.Instance;
            couchConn.CouchConn = value;
            var rc = await couchClient.getCouchDbVer();
            return rc;

        }
    }
}