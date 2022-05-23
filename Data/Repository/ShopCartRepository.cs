using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EMarket.Data.Repository
{
    public class ShopCartRepository : IAllShopCarts
    {
        private readonly AppDbContext AppDbContext;
        private readonly ShopCart ShopCart;
        public ShopCartRepository(AppDbContext appDbContext, ShopCart shopCart)
        {
            this.AppDbContext = appDbContext;
            this.ShopCart = shopCart;
        }

        public List<ShopCartItem> ListShopCartItems => AppDbContext.ShopCartItem.Where(p => p.ShopCartId == ShopCart.ShopCartId).Include(p => p.Monitor).ToList();
        
        public void AddToCart(Monitor monitor, int amount)
        {
            if (ListShopCartItems.Count == 0)
            {
                AppDbContext.ShopCartItem.Add(new ShopCartItem
                {
                    ShopCartId = ShopCart.ShopCartId,
                    Monitor = monitor,
                    Price = monitor.Price,
                    Amount = amount,
                });
            }
            else
            {
                int count = 0;
                foreach (var item in ListShopCartItems)
                {

                    if (item.Monitor.Id == monitor.Id)
                    {
                        count++;
                        item.Amount += amount;
                    }
                }
                if (count == 0)
                    AppDbContext.ShopCartItem.Add(new ShopCartItem
                    {
                        ShopCartId = ShopCart.ShopCartId,
                        Monitor = monitor,
                        Price = monitor.Price,
                        Amount = amount
                    });
            }
            AppDbContext.SaveChanges();
        }

        public void RemoveItem(Monitor monitor)
        {
            var shopCartItemDel = AppDbContext.ShopCartItem.FirstOrDefault(item => item.Monitor.Id == monitor.Id);
            if (shopCartItemDel != null)
            {
                if (shopCartItemDel.Amount > 1)
                    shopCartItemDel.Amount -= 1;
                else
                    AppDbContext.ShopCartItem.Remove(shopCartItemDel);

                AppDbContext.SaveChanges();
            }
        }

        public void ClearShopcart()
        {            
            AppDbContext.ShopCartItem.RemoveRange(ListShopCartItems);
            AppDbContext.SaveChanges();
        }

        public decimal TotalValueCalculation()
        {
            return ListShopCartItems.Sum(e => e.Monitor.Price * e.Amount);
        }        
    }
}
