using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamBusLib.Models.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataCouchDBBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommPortConfController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CommPortConf>> Get()
        {
            var list = new List<CommPortConf>();
            var comConf = new CommPortConf();
            comConf.BaudRate = 57600;
            comConf.PortName = "com20";
            comConf.Parity = "None";
            comConf.DataBits = 8;
            comConf.Handshake = "none";
            comConf.StopBits = "One";
            comConf.ReadTimeout = 5000;
            comConf.WriteTimeout = 500;
            return list;
        }
    }
}