using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using EMarket.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EMarket.Controllers
{
    public class DetailsMonitorController : Controller
    {        
        private readonly IAllMonitors MonitorRep;        
               
        public DetailsMonitorController(IAllMonitors monitorRep)
        {
            MonitorRep = monitorRep;            
        }       

        [HttpGet]
        public ActionResult OpenInfo(int id)
        {
            Monitor monitor = MonitorRep.GetObjectMonitor(id);
            var detailsMonitor = new DetailsMonitorViewModel { DetailsMonitor = monitor };
            return View(detailsMonitor);            
        }
    }
}

