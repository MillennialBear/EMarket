using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMarket.Data.Models
{
    public class Monitor
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Product name")]
        public string Name { get; set; }

        [Display(Name = "Diagonal in inch")]
        public double Diagonal { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Short description")]
        public string ShortDesc { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Long Description")]
        public string LongDesc { get; set; }

        [Display(Name = "Path of image")]
        public string Img { get; set; }

        [Display(Name = "Price")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Display(Name = "Popular")]
        public bool IsFavourite { get; set; }

        [Display(Name = "Avalible")]
        public bool Avalible { get; set; }

        [Display(Name = "ID category")]
        public int CategoryId { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }        

        [Display(Name = "Category")]
        public virtual Category Category { get; set; }        
    }    
}
