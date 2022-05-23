using System.ComponentModel.DataAnnotations.Schema;

namespace EMarket.Data.Models
{
    public class ShopCartItem
    {
        public int Id { get; set; }

        public virtual Monitor Monitor { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string ShopCartId { get; set; }
    }
}
