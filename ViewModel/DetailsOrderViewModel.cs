using EMarket.Data.Models;
using System.Collections.Generic;

namespace EMarket.ViewModel
{
    public class DetailsOrderViewModel
    {        
        public Order Order { get; set; }  
        
        public IList<OrderDetail> DetailsOrderList { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
