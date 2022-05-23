using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using EMarket.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace EMarket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagementController : Controller
    {
        private readonly IAllMonitors AllMonitors;
        private readonly IMonitorsCategory MonitorsCategory;

        public ManagementController(IAllMonitors allMonitors, IMonitorsCategory monitorsCategory)
        {
            AllMonitors = allMonitors;
            MonitorsCategory = monitorsCategory;
        }
                
        public IActionResult Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] =
                String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewData["CategoryIdSortParm"] =
                sortOrder == "CategoryId" ? "CategoryId_desc" : "CategoryId";
            ViewData["CategorySortParm"] =
                sortOrder == "Category" ? "Category_desc" : "Category";
            ViewData["NameSortParm"] =
                sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["DiagonalSortParm"] =
                sortOrder == "Diagonal" ? "Diagonal_desc" : "Diagonal";
            ViewData["PriceSortParm"] =
                sortOrder == "Price" ? "Price_desc" : "Price";
            ViewData["ColorSortParm"] =
                sortOrder == "Color" ? "Color_desc" : "Color";

            if (searchString != null)            
                pageNumber = 1;            
            else            
                searchString = currentFilter;            

            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Monitor> monitors = AllMonitors.Monitors;

            if (!String.IsNullOrEmpty(searchString))            
                monitors = monitors.Where(s => s.Name.ToLower().Contains(searchString.Trim().ToLower()));            

            if (string.IsNullOrEmpty(sortOrder))            
                sortOrder = "Id";
            
            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)            
                monitors = monitors.AsQueryable().OrderBy(sortOrder + " desc").ToList();            
            else            
                monitors = monitors.AsQueryable().OrderBy(sortOrder + " asc").ToList();            

            int pageSize = 3;
            return View(PaginatedList<Monitor>.Create(monitors,
                pageNumber ?? 1, pageSize));
        }

        public IActionResult Details(int id)
        {
            var monitor = AllMonitors.GetObjectMonitor(id);
            int catId = monitor.CategoryId;
            Category currentCategory = MonitorsCategory.GetCategory(catId);

            if (monitor == null)            
                return NotFound();            

            var monitorObj = new DetailsMonitorViewModel
            {
                DetailsMonitor = monitor,
                Category = currentCategory,
            };

            return View(monitorObj);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            [Bind("Id, CategoryId, Category, Name, Diagonal, ShortDesc, LongDesc, Color, Img, Price, IsFavourite, Avalible")] Monitor monitor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AllMonitors.Create(monitor);
                    AllMonitors.Save();
                    TempData["message"] = string.Format("Product \"{0}\" was been created",
                    monitor.Name);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the " +
                    "problem persists see your system administrator.");
            }

            return View(monitor);
        }

        public IActionResult Edit(int id)
        {
            var monitor = AllMonitors.GetObjectMonitor(id);
            int catId = monitor.CategoryId;
            Category currentCategory = MonitorsCategory.GetCategory(catId);

            if (monitor == null)            
                return NotFound();            

            return View(monitor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("Id, CategoryId, Category, Name, Diagonal, ShortDesc, LongDesc, Color, Img, Price, IsFavourite, Avalible")] Monitor monitor)
        {

            if (id != monitor.Id)
                return NotFound();            

            if (ModelState.IsValid)
            {
                try
                {
                    AllMonitors.Update(monitor);                    
                    AllMonitors.Save();                    
                }
                catch (DataException ex)
                {
                    if (!AllMonitors.MonitorExists(monitor.Id))                    
                        return NotFound();                    
                    else                    
                        throw;                    
                }
                TempData["message"] = string.Format("Changes for product \"{0}\" was been saved", monitor.Name);
                return RedirectToAction("Index");
            }
            return View(monitor);
        }

        public IActionResult Delete(int id)
        {
            var monitor = AllMonitors.GetObjectMonitor(id);
            int catId = monitor.CategoryId;
            Category currentCategory = MonitorsCategory.GetCategory(catId);

            if (monitor == null)            
                return NotFound();            

            return View(monitor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var monitor = AllMonitors.GetObjectMonitor(id);
            AllMonitors.Delete(id);
            AllMonitors.Save();

            TempData["message"] = string.Format("Product \"{0}\" was been deleted",
                    monitor.Name);
            return RedirectToAction(nameof(Index));
        }

        //protected override void Dispose(bool disposing)
        //{
        //    AllMonitors.Monitors.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
