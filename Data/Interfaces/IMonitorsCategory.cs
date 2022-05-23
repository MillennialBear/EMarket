using EMarket.Data.Models;
using System.Collections.Generic;

namespace EMarket.Data.Interfaces
{
    public interface IMonitorsCategory
    {
        IEnumerable<Category> AllCategory { get; }

        Category GetCategory (int catId);               
    }
}
