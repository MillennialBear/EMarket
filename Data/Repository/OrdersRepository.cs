using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMarket.Service;

namespace EMarket.Data.Repository
{    
    public class OrdersRepository : IAllOrders
    {
        private readonly AppDbContext AppDbContext;
        private readonly ShopCart ShopCart;
        private readonly IAllShopCarts ShopCartsRep;
        
        public OrdersRepository(AppDbContext appDbContext, ShopCart shopCart, IAllShopCarts shopCartsRep)
        {
            this.AppDbContext = appDbContext;
            this.ShopCart = shopCart;
            this.ShopCartsRep = shopCartsRep;           
        }

        public IEnumerable<Order> Orders => AppDbContext.Order.Include(p => p.OrderDetail);
        public Order GetOrder(int orderId) => AppDbContext.Order.FirstOrDefault(s => s.Id == orderId);
        
        public void Save()
        {
            AppDbContext.SaveChanges();
        }

        public void Update(Order order)
        {
            AppDbContext.Entry(order).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Order order = AppDbContext.Order.Find(id);
            if (order != null)
                AppDbContext.Order.Remove(order);
        }

        public bool OrderExists(int id)
        {
            return AppDbContext.Order.Any(e => e.Id == id);
        }
        
        public void CreateOrder(Order order)
        {
            order.TimeCreated = DateTime.Now;
            AppDbContext.Order.Add(order);
            AppDbContext.SaveChanges();

            var items = ShopCartsRep.ListShopCartItems;
            foreach (var el in items)
            {
                var orderDetail = new OrderDetail()
                {
                    MonitorID = el.Monitor.Id,
                    OrderID = order.Id,
                    Amount = el.Amount,
                    UnitPrice = el.Price,
                    Price = el.Monitor.Price * el.Amount,
                };
                AppDbContext.OrderDetail.Add(orderDetail);
            }
            AppDbContext.SaveChanges();

            StringBuilder body = new StringBuilder(255, 1024)
                    .Append("<center>").AppendLine("New order processed").Append("</center>").Append("<br>")
                    .AppendLine("---").Append("<br>")
                    .AppendLine("Products:").Append("<br>"); ;

            foreach (var line in items)
            {
                var subtotal = line.Monitor.Price * line.Amount;
                body.AppendFormat("{0} x {1} (Total: {2:c})",
                    line.Amount, line.Monitor.Name, subtotal).Append("<br>");
            }

            body.AppendFormat("Total price: {0:c}", ShopCartsRep.TotalValueCalculation()).Append("<br>")
            .AppendLine("---").Append("<br>")
            .AppendLine("Delivery:").Append("<br>")
            .AppendLine(order.Name).Append("<br>")
            .AppendLine(order.Surname).Append("<br>")
            .AppendLine(order.Address).Append("<br>")
            .AppendLine(order.Phone).Append("<br>")
            .AppendLine(order.Email).Append("<br>")
            .AppendLine("---");

            EmailService emailService = new EmailService();

            var str = order.Email;
            emailService.SendEmail(str, "Information for your order", body);
        }
    }
}