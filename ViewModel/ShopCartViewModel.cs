using EMarket.Data.Models;
using System.Collections.Generic;

namespace EMarket.ViewModel
{
    public class ShopCartViewModel
    {
        public ShopCart ShopCart { get; set; }

        public IList<ShopCartItem> ShopCartItems { get; set; }

        public decimal CartTotal { get; set; }        
    }
}
