using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace EMarket.Data.Repository
{
    public class CategoryRepository : IMonitorsCategory
    {
        private readonly AppDbContext AppDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            this.AppDbContext = appDbContext;
        }

        public IEnumerable<Category> AllCategory => AppDbContext.Category;

        public Category GetCategory(int catId) => AppDbContext.Category.FirstOrDefault(s => s.Id == catId);        
    }
}
