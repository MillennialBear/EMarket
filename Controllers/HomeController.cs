using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using EMarket.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllMonitors MonitorRep;

        public HomeController(IAllMonitors monitorRep)
        {
            MonitorRep = monitorRep;            
        }

        public ViewResult Index()
        {
            ViewBag.Title = "iiyama";
            List<Monitor> favMonitors = new (4);

            var touchFavmonitor = MonitorRep.GetFavouriteMonitors.Where(m => m.CategoryId == 1).ToList();
            favMonitors.Add(FavRandomMonitor(touchFavmonitor));

            var desktopFavmonitor = MonitorRep.GetFavouriteMonitors.Where(m => m.CategoryId == 2).ToList();
            favMonitors.Add(FavRandomMonitor(desktopFavmonitor));

            var lfdFavmonitor = MonitorRep.GetFavouriteMonitors.Where(m => m.CategoryId == 3).ToList();
            favMonitors.Add(FavRandomMonitor(lfdFavmonitor));

            var gameFavmonitor = MonitorRep.GetFavouriteMonitors.Where(m => m.CategoryId == 4).ToList();
            favMonitors.Add(FavRandomMonitor(gameFavmonitor));

            Monitor FavRandomMonitor(IList<Monitor> typeFavMonitors)
            {
                Random random = new Random();
                int index = 1;                
                index = random.Next(0, typeFavMonitors.Count);
                Monitor monitor = typeFavMonitors[index];
                return monitor;
            }
            
            var homeMonitors = new HomeViewModel { FavMonitors = favMonitors };
            return View(homeMonitors);
        }

        public ViewResult Support()
        {
            ViewBag.Title = "iiyama support";
            return View();
        }

        public ViewResult Press()
        {
            ViewBag.Title = "Press";
            return View();
        }

        public ViewResult Privacy()
        {
            ViewBag.Title = "Privacy policy";
            return View();
        }

        public ViewResult Company()
        {
            ViewBag.Title = "Company";
            return View();
        }
    }
}
