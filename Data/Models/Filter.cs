using System.ComponentModel.DataAnnotations.Schema;

namespace EMarket.Data.Models
{
    public class Filter
    {
        public int Id { get; set; }

        public string NameFilter { get; set; }

        public string FilterDiagonal { get; set; }

        public string FilterColor { get; set; }
        
        [NotMapped]
        public bool CheckboxAnswer { get; set; }
    }
}
