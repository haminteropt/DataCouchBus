using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HamBusLib.Models.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DataCouchDBBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommPortConfController : ControllerBase
    {
        private readonly ILogger logger;
        public CommPortConfController(ILogger<CommPortConfController> logger)
        {
            this.logger = logger;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CommPortConf>> Get()
        {
            logger.LogInformation("GET comm config");
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
            list.Add(comConf);
            return list;
        }
    }
}