using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMarket.Data.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string Desc { get; set; }  
        
        public IEnumerable<Monitor> AllMonitors { get; set; }

        [NotMapped]
        public bool CheckboxAnswer { get; set; }

    }
}
