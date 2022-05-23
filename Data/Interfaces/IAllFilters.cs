using EMarket.Data.Models;
using System.Collections.Generic;

namespace EMarket.Data.Interfaces
{
    public interface IAllFilters
    {
        IEnumerable<Filter> AllFilter { get; }

        IEnumerable<Filter> AllFilterDiagonal { get; }

        IEnumerable<Filter> AllFilterColor { get; }        
    }
}
