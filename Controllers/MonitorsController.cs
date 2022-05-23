using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using EMarket.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace EMarket.Controllers
{
    public class MonitorsController : Controller
    {
        private readonly IAllMonitors AllMonitors;
        private readonly IMonitorsCategory MonitorsCategory;
        private readonly IAllFilters MonitorsFilters;
        List<Monitor> tempListCategory = new List<Monitor>();
        List<Monitor> tempListColor = new List<Monitor>();
        List<Monitor> tempListDiagonal = new List<Monitor>();
        int checkedEmptyList = 0;

        public MonitorsController(IAllMonitors allMonitors, IMonitorsCategory monitorsCategory, IAllFilters monitorFilters)
        {
            AllMonitors = allMonitors;
            MonitorsCategory = monitorsCategory;
            MonitorsFilters = monitorFilters;
        }

        [HttpGet]
        [Route("Monitors/List")]
        [Route("Monitors/List/{category}")]
        public IActionResult List(string searchString, string category)
        {
            ViewBag.Title = "iiayma monitors";
            IEnumerable<Monitor> monitors = AllMonitors.Monitors;
            IList<Category> categoryList = MonitorsCategory.AllCategory.ToList();
            IList <Filter> filterListDiagonal = MonitorsFilters.AllFilterDiagonal.ToList();
            IList <Filter> filterListColor = MonitorsFilters.AllFilterColor.ToList();
            string _searchString = searchString;
            string _category = category;
            string currentCategory = "";

            if (!String.IsNullOrEmpty(_searchString))
            {
                ViewBag.Title = "Results on your request";
                ViewBag.ResultSearch = "Results on your request:";
                monitors = monitors.Where(s => s.Name.ToLower().Contains(searchString.Trim().ToLower()));
                if (monitors.Count() == 0)
                {
                    ViewBag.NotFoundResultSearch = "Products not found! Try changing the query or choose from the following products:";
                    monitors = AllMonitors.Monitors.OrderBy(i => i.IsFavourite);
                }
            }
            if(!String.IsNullOrEmpty(_category))
            {
                if (string.Equals("touchscreen", category, StringComparison.OrdinalIgnoreCase))
                {
                    monitors = AllMonitors.Monitors.Where(i => i.Category.CategoryName.Equals("Touchscreen")).OrderBy(i => i.Id);
                    currentCategory = "Touchscreen";
                    ViewBag.Title = "Touchscreen";
                }
                else if (string.Equals("desktop", category, StringComparison.OrdinalIgnoreCase))
                {
                    monitors = AllMonitors.Monitors.Where(i => i.Category.CategoryName.Equals("Desktop")).OrderBy(i => i.Id);
                    currentCategory = "Desktop monitors";
                    ViewBag.Title = "Desktop monitors";
                }
                else if (string.Equals("lfd", category, StringComparison.OrdinalIgnoreCase))
                {
                    monitors = AllMonitors.Monitors.Where(i => i.Category.CategoryName.Equals("LFD")).OrderBy(i => i.Id);
                    currentCategory = "LFD monitors";
                    ViewBag.Title = "LFD monitors";
                }
                else if (string.Equals("gaming", category, StringComparison.OrdinalIgnoreCase))
                {
                    monitors = AllMonitors.Monitors.Where(i => i.Category.CategoryName.Equals("Gaming")).OrderBy(i => i.Id);
                    currentCategory = "Gaming monitors";
                    ViewBag.Title = "Gaming monitors";
                }
            }

            var monitorObj = new MonitorsListViewModel
            {
                AllMonitors = monitors,
                CurrentCategory = currentCategory,
                Categories = categoryList,
                DiagonalFilters = filterListDiagonal,
                ColorFilters = filterListColor                              
            }; 

            return View(monitorObj);
        }

        [HttpPost]        
        public IActionResult List(MonitorsListViewModel objFilterCat)            
        {            
            IEnumerable<Monitor> monitors = AllMonitors.Monitors;                     

            if (objFilterCat.Categories != null)
            {
                for (var i = 0; i < objFilterCat.Categories.Count(); i++)
                {
                    if (objFilterCat.Categories[i].CheckboxAnswer == true)
                    {
                        var allCheckCategoryMonitors = AllMonitors.Monitors.Where(t => t.Category.CategoryName == objFilterCat.Categories[i].CategoryName);                        
                        CheckingEmptyList(allCheckCategoryMonitors);
                        foreach (var item in allCheckCategoryMonitors)
                            tempListCategory.Add(item);
                    }
                }
            }            

            if (objFilterCat.DiagonalFilters != null)
            {
                for (var j = 0; j < objFilterCat.DiagonalFilters.Count; j++)
                {
                    if (objFilterCat.DiagonalFilters[j].CheckboxAnswer == true)
                    {
                        if (objFilterCat.DiagonalFilters[j].FilterDiagonal.Contains("small"))
                        {
                            var allCheckDiagonalMonitors = AllMonitors.Monitors.TakeWhile(t => t.Diagonal <= 15);
                            CheckingEmptyList(allCheckDiagonalMonitors);
                            AddCheckDiagonalItem(allCheckDiagonalMonitors);
                        }
                        else if (objFilterCat.DiagonalFilters[j].FilterDiagonal.Contains("middle"))
                        {
                            var allCheckDiagonalMonitors = AllMonitors.Monitors.SkipWhile(t => t.Diagonal < 15.1).TakeWhile(t => t.Diagonal <= 30);
                            CheckingEmptyList(allCheckDiagonalMonitors);
                            AddCheckDiagonalItem(allCheckDiagonalMonitors);
                        }
                        else if (objFilterCat.DiagonalFilters[j].FilterDiagonal.Contains("large"))
                        {
                            var allCheckDiagonalMonitors = AllMonitors.Monitors.SkipWhile(t => t.Diagonal < 30.1).TakeWhile(t => t.Diagonal <= 50);
                            CheckingEmptyList(allCheckDiagonalMonitors);
                            AddCheckDiagonalItem(allCheckDiagonalMonitors);                            
                        }
                        else if (objFilterCat.DiagonalFilters[j].FilterDiagonal.Contains("huge"))
                        {
                            var allCheckDiagonalMonitors = AllMonitors.Monitors.SkipWhile(t => t.Diagonal < 50.1);
                            CheckingEmptyList(allCheckDiagonalMonitors);
                            AddCheckDiagonalItem(allCheckDiagonalMonitors);
                        }                        
                    }
                }
            }            

            if (objFilterCat.ColorFilters != null)
            {
                for (var k = 0; k < objFilterCat.ColorFilters.Count; k++)
                {
                    if (objFilterCat.ColorFilters[k].CheckboxAnswer == true)
                    {
                        if (objFilterCat.ColorFilters[k].FilterColor.Contains("White"))
                        {
                            var allCheckColorMonitors = AllMonitors.Monitors.Where(t => t.Color.ToLower() == "white");
                            CheckingEmptyList(allCheckColorMonitors);                            
                            foreach (var item in allCheckColorMonitors)
                                tempListColor.Add(item);
                        }
                        if (objFilterCat.ColorFilters[k].FilterColor.Contains("Black"))
                        {
                            var allCheckColorMonitors = AllMonitors.Monitors.Where(t => t.Color.ToLower() == "black");
                            CheckingEmptyList(allCheckColorMonitors);                            
                            foreach (var item in allCheckColorMonitors)
                                tempListColor.Add(item);
                        }
                    }
                }
            }

            monitors = FormationListMatchingItems(monitors);

            if (checkedEmptyList > 0 || monitors.Count() == 0)
            {                
                monitors = new List<Monitor>();
                ViewBag.ResultFilters = "There are no products matching your selection. Сhange your filter options.";
            }

            if (monitors != null)
            {
                string selectValue = objFilterCat.Dropdown.Value;

                if (string.IsNullOrEmpty(selectValue))
                    selectValue = "IsFavourite";

                bool descending = false;
                if (selectValue.EndsWith("_desc"))
                {
                    selectValue = selectValue.Substring(0, selectValue.Length - 5);
                    descending = true;
                }

                if (descending)
                    monitors = monitors.AsQueryable().OrderBy(selectValue + " desc").ToList();
                else
                    monitors = monitors.AsQueryable().OrderBy(selectValue + " asc").ToList();
            }            

            var monitorObj = new MonitorsListViewModel
            {
                AllMonitors = monitors,
                Categories = objFilterCat.Categories,
                DiagonalFilters = objFilterCat.DiagonalFilters,
                ColorFilters = objFilterCat.ColorFilters,
                Dropdown = objFilterCat.Dropdown,
            };            

            return View(monitorObj);
        }

        void AddCheckDiagonalItem(IEnumerable<Monitor> allCheckItem)
        {
            foreach (var item in allCheckItem)
                tempListDiagonal.Add(item);
        }

        void CheckingEmptyList(IEnumerable<Monitor> allCheckList)
        {
            if (allCheckList.Count() == 0)
                checkedEmptyList++;
        }

        IEnumerable<Monitor> FormationListMatchingItems(IEnumerable<Monitor> monitors)
        {
            int a = CheckingCountItem(tempListCategory);
            int b = CheckingCountItem(tempListDiagonal);
            int c = CheckingCountItem(tempListColor);
            int sum = a + b + c;

            if (sum == 1)
            {
                if (a > 0)
                    monitors = tempListCategory;
                else if (b > 0)
                    monitors = tempListDiagonal;
                else if (c > 0)
                    monitors = tempListColor;
                ViewBag.Title = "Filters monitors";
            }
            else
            {
                var tempMonitors = tempListCategory.Concat(tempListDiagonal).Concat(tempListColor);

                if (sum == 2)
                {
                    List<Monitor> listMatchesItems = new List<Monitor>();
                    HashSet<Monitor> hash = new HashSet<Monitor>();
                    foreach (Monitor item in tempMonitors)
                    {
                        if (!hash.Add(item))
                            listMatchesItems.Add(item);
                    }
                    ViewBag.Title = "Filters monitors";
                    monitors = listMatchesItems;
                }
                else if (sum == 3)
                {
                    List<Monitor> listMatchesItems = new List<Monitor>();
                    HashSet<Monitor> hash = new HashSet<Monitor>();
                    foreach (Monitor item in tempMonitors)
                    {
                        if (!hash.Add(item))
                            listMatchesItems.Add(item);
                    }
                    List<Monitor> secondListMatchesItems = new List<Monitor>();
                    HashSet<Monitor> hash2 = new HashSet<Monitor>();
                    foreach (Monitor item in listMatchesItems)
                    {
                        if (!hash2.Add(item))
                            secondListMatchesItems.Add(item);
                    }
                    ViewBag.Title = "Filters monitors";
                    monitors = secondListMatchesItems;
                }
            }
            return monitors;
        }

        int CheckingCountItem(List<Monitor> tempListFilter)
        {
            if (tempListFilter.Count() == 0)
                return 0;
            return 1;
        }
    }
}

//protected override void Dispose(bool disposing)
//{
//    AllMonitors.Dispose();
//    base.Dispose(disposing);
//}