using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sheduler.Core.Interfaces;

namespace ShedulerWeb.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShedulerController : ControllerBase
    {
        private readonly IDllSheduler _dllSheduler;

        public ShedulerController(IDllSheduler dllSheduler)
        {
            _dllSheduler = dllSheduler;
        }
        [Route("/start")]
        public string Start()
        {
            _dllSheduler.Start();
            return "started";
        }

        [Route("/stop")]
        public string Stop()
        {
            _dllSheduler.Stop();
            return "stop";
        }

        [Route("/status")]
        public string Status()
        {
            return _dllSheduler.IsStarted.ToString();
        }
    }
}