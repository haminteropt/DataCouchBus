using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataCouchDBBus.CouchDB;
using HamBusLib.DataBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataCouchDBBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] DataConnection value)
        {
            var couchConn = CouchConnection.Instance;
            couchConn.CouchConn = value;
        }
    }
}