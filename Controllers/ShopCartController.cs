using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using EMarket.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EMarket.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly IAllMonitors MonitorRep;
        private readonly IAllShopCarts ShopCartRep;        

        public ShopCartController(IAllMonitors monitorRep, IAllShopCarts shopCartRep)
        {
            MonitorRep = monitorRep;
            ShopCartRep = shopCartRep;            
        }

        public ViewResult Index()
        {
            List<ShopCartItem> items = ShopCartRep.ListShopCartItems;
            var price = ShopCartRep.TotalValueCalculation();
            
            ViewBag.Title = "Shop cart";
            var obj = new ShopCartViewModel { ShopCartItems = items, CartTotal = price };
            return View(obj);
        }

        public RedirectToActionResult AddCart(int id, int amount)
        {            
            var monitor = MonitorRep.Monitors.FirstOrDefault(x => x.Id == id);            
            if (monitor != null)
            {                
                ShopCartRep.AddToCart(monitor, 1);
            }

            List<ShopCartItem> items = ShopCartRep.ListShopCartItems;            
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromCart(int id)
        {
            var monitor = MonitorRep.Monitors.FirstOrDefault(x => x.Id == id);
            if(monitor != null)
            {                
                ShopCartRep.RemoveItem(monitor);
            }            

            return RedirectToAction("Index");            
        }

        public RedirectToActionResult ClearCart()
        {
            ShopCartRep.ClearShopcart();            
            return RedirectToAction("Index");
        }
    }
}
