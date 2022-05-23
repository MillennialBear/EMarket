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
    public class OrderManagement : Controller
    {              
        private readonly IAllOrders AllOrders;
        private readonly IOrderDetail OrderDetail;

        public OrderManagement(IAllOrders allOrders, IOrderDetail orderDetail)
        {            
            AllOrders = allOrders;
            OrderDetail = orderDetail;
        }

        public IActionResult IndexOrder(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] =
                String.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewData["NameSortParm"] =
                sortOrder == "Name" ? "Name_desc" : "Name";
            ViewData["SurnameSortParm"] =
                sortOrder == "Surname" ? "Surname_desc" : "Surname";
            ViewData["AddressSortParm"] =
                sortOrder == "Adress" ? "Address_desc" : "Address";
            ViewData["PhoneSortParm"] =
                sortOrder == "Phone" ? "Phone_desc" : "Phone";
            ViewData["EmailSortParm"] =
                sortOrder == "Email" ? "Email_desc" : "Email";
            ViewData["TimeCreatedSortParm"] =
                sortOrder == "TimeCreated" ? "TimeCreated_desc" : "TimeCreated";

            if (searchString != null)
                pageNumber = 1;
            else
                searchString = currentFilter;

            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Order> orders = AllOrders.Orders;

            if (!String.IsNullOrEmpty(searchString))
                orders = orders.Where(s => s.Name.ToLower().Contains(searchString.Trim().ToLower()));

            if (string.IsNullOrEmpty(sortOrder))
                sortOrder = "Id";

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
                orders = orders.AsQueryable().OrderBy(sortOrder + " desc").ToList();
            else
                orders = orders.AsQueryable().OrderBy(sortOrder + " asc").ToList();

            int pageSize = 10;
            return View(PaginatedList<Order>.Create(orders,
                pageNumber ?? 1, pageSize));
        }

        public IActionResult DetailsOrder(int id)
        {            
            var order = AllOrders.GetOrder(id);
            var detailsOrderList = OrderDetail.ListOrderDetail(id);
            var price = detailsOrderList.Sum(e => e.Monitor.Price * e.Amount);

            if (order == null)
                return NotFound();

            var orderObj = new DetailsOrderViewModel
            {                
                Order = order,
                DetailsOrderList = detailsOrderList,
                TotalPrice = price,
                
            };

            return View(orderObj);
        }        

        public IActionResult EditOrder(int id)
        {
            var order = AllOrders.GetOrder(id);
            var detailsOrderList = OrderDetail.ListOrderDetail(id);
            var price = detailsOrderList.Sum(e => e.Monitor.Price * e.Amount);

            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOrder(int id,
            [Bind("Id, Name, Surname, Address, Phone, Email, TimeCreated")] Order order)
        {

            if (id != order.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    AllOrders.Update(order);
                    AllOrders.Save();
                }
                catch (DataException ex)
                {
                    if (!AllOrders.OrderExists(order.Id))
                        return NotFound();
                    else
                        throw;
                }
                TempData["message"] = string.Format("Changes for order \"{0}\" was been saved", order.Id);
                return RedirectToAction("IndexOrder");
            }
            return View(order);
        }

        public IActionResult DeleteOrder(int id)
        {
            var order = AllOrders.GetOrder(id);
            
            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost, ActionName("DeleteOrder")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var order = AllOrders.GetOrder(id);
            AllOrders.Delete(id);
            AllOrders.Save();

            TempData["message"] = string.Format("Order N\"{0}\" was been deleted",
                    order.Id);
            return RedirectToAction(nameof(IndexOrder));
        }

        //protected override void Dispose(bool disposing)
        //{
        //    AllMonitors.Dispose();
        //    base.Dispose(disposing);
        //}
    }
}
