using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Sheduler.Core.Configuration;
using Sheduler.Core.Interfaces;

namespace ShedulerWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDllSheduler _dllSheduler;
        private readonly ShedulerSetting _shedulerSetting;


        public IndexModel(IOptions<ShedulerSetting> shedulerSetting, IDllSheduler sheduler)
        {
            _dllSheduler = sheduler;
            _shedulerSetting = shedulerSetting.Value;
        }
        public void OnGet()
        {
            ViewData["Status"] = _dllSheduler.IsStarted;
        }
    }
}
