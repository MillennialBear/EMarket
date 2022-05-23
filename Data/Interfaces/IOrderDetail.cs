using EMarket.Data.Models;
using System.Collections.Generic;

namespace EMarket.Data.Interfaces
{
    public interface IOrderDetail
    {        
        List<OrderDetail> ListOrderDetail(int id);        
    }
}
