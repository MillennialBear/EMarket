using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EMarket.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAllOrders AllOrders;
        private readonly IAllShopCarts AllShopCarts;
        private readonly ShopCart ShopCart;
        public OrderController(IAllOrders allOrders, ShopCart shopCart, IAllShopCarts allShopCarts)
        {
            this.AllOrders = allOrders;
            this.ShopCart = shopCart;
            this.AllShopCarts = allShopCarts;
        }

        public IActionResult Checkout()
        {
            ViewBag.Title = "Ordering";
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {            
            List<ShopCartItem> items = AllShopCarts.ListShopCartItems;            
            if (items.Count == 0)
                ModelState.AddModelError("", "Add items to cart");

            if (ModelState.IsValid)
            {
                AllOrders.CreateOrder(order);
                AllShopCarts.ClearShopcart();
                return RedirectToAction("Complete");
            }
            return View(order);
        }

        public IActionResult Complete()
        {
            ViewBag.Title = "Confirmed";
            ViewBag.Message = "Order successfully processed!";
            return View();
        }
    }
}
