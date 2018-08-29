using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Sheduler.Infrastructure.Settings;

namespace ShedulerWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ShedulerSetting _shedulerSetting;


        public IndexModel(IOptions<ShedulerSetting> shedulerSetting)
        {
            _shedulerSetting = shedulerSetting.Value;
        }
        public void OnGet()
        {

        }
    }
}
