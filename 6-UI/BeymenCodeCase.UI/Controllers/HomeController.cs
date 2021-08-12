using BeymenCodeCase.Models;
using BeymenCodeCase.Services.Configuration;
using BeymenCodeCase.Services.Helper;
using BeymenCodeCase.Services.Redis;
using BeymenCodeCase.Services.Services;
using BeymenCodeCase.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BeymenCodeCase.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigurationService configurationService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IConfigurationService configurationService)
        {

            this.configurationService = configurationService;
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            //var service = ServiceHelper.GetService<IRedisService>();
            //service.SetCacheValueAsync("application_name", "1313");
            //var data = service.GetValueCacheValueAsync("application_name");
            //configurationService.SaveConfiguration(new ApplicationConfigurationModel
            //{
            //    ApplicationName = "sevice-a",
            //    IsActive = true,
            //    Name = "service-a",
            //    Value = "boynet.com.tr"
            //});

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
