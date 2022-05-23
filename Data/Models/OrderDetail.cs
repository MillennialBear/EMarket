using System.ComponentModel.DataAnnotations.Schema;

namespace EMarket.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderID { get; set; }

        public int MonitorID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Amount { get; set; }

        public virtual Monitor Monitor { get; set; }  
        
        public virtual Order Order { get; set; }
    }
}
