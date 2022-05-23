using EMarket.Data.Models;
using System.Collections.Generic;

namespace EMarket.Data.Interfaces
{
    public interface IAllOrders
    {
        void CreateOrder(Order order);

        IEnumerable<Order> Orders { get; }

        Order GetOrder(int orderId);    
        
        void Save();

        void Update(Order order);

        void Delete(int id);

        bool OrderExists(int id);
    }
}
