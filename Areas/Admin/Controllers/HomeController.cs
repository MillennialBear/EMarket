using Microsoft.AspNetCore.Mvc;
using EMarket.Data.Interfaces;

namespace EMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IAllMonitors MonitorRep;        
        
        public HomeController(IAllMonitors monitorRep)
        {
            MonitorRep = monitorRep;
        }

        public IActionResult AdminIndex()
        {
            return View();
        }
    }
}