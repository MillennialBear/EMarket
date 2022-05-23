using EMarket.Data.Models;
using System.Collections.Generic;

namespace EMarket.ViewModel
{
    public class MonitorsListViewModel
    {
        public IEnumerable<Monitor> AllMonitors { get; set; }

        public string CurrentCategory { get; set; }

        public IList<Category> Categories { get; set; }

        public IList<Filter> DiagonalFilters { get; set; }

        public IList<Filter> ColorFilters { get; set; }

        public Dropdown Dropdown { get; set; }       
    }
}
