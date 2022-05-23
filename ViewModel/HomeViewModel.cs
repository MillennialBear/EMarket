using EMarket.Data.Models;
using System.Collections.Generic;

namespace EMarket.ViewModel
{
    public class HomeViewModel
    {
        public IList<Monitor> FavMonitors { get; set; }

        public Monitor FavMonitor { get; set; }
    }
}
