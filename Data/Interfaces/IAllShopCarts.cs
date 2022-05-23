using EMarket.Data.Models;
using System.Collections.Generic;

namespace EMarket.Data.Interfaces
{
    public interface IAllShopCarts
    {
        List<ShopCartItem> ListShopCartItems { get; }  
        
        void AddToCart(Monitor monitor, int amount);

        void RemoveItem(Monitor monitor);

        void ClearShopcart();

        decimal TotalValueCalculation();
    }
}
