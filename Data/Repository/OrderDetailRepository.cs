using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EMarket.Data.Repository
{
    public class OrderDetailRepository : IOrderDetail
    {
        private readonly AppDbContext AppDbContext;        

        public OrderDetailRepository(AppDbContext appDbContext)
        {
            this.AppDbContext = appDbContext;            
        }
        
        public List<OrderDetail> ListOrderDetail(int orderId) => AppDbContext.OrderDetail.Where(p => p.OrderID == orderId).Include(p => p.Monitor).ToList();                
    }
}
