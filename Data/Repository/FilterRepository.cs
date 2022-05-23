using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace EMarket.Data.Repository
{
    public class FilterRepository : IAllFilters
    {
        private readonly AppDbContext AppDbContext;

        public FilterRepository(AppDbContext appDbContext)
        {
            this.AppDbContext = appDbContext;
        }

        public IEnumerable<Filter> AllFilter => AppDbContext.Filters;

        public IEnumerable<Filter> AllFilterDiagonal => AppDbContext.Filters.Where(p => p.FilterDiagonal != null);

        public IEnumerable<Filter> AllFilterColor => AppDbContext.Filters.Where(p => p.FilterColor != null);
        
    }
}
