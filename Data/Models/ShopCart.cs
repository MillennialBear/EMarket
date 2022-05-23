using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EMarket.Data.Models
{
    public class ShopCart
    {
        private readonly AppDbContext AppDbContext;

        public ShopCart(AppDbContext appDbContext)
        {
            this.AppDbContext = appDbContext;
        }

        public string ShopCartId { get; set; }  
        
        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDbContext>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", shopCartId);

            return new ShopCart(context) { ShopCartId = shopCartId };
        }        
    }
}
