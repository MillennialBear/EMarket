using EMarket.Data.Interfaces;
using EMarket.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EMarket.Data.Repository
{
    public class MonitorRepository : IAllMonitors
    {
        private readonly AppDbContext AppDbContext;

        public MonitorRepository(AppDbContext appDbContext)
        {
            this.AppDbContext = appDbContext;
        }
        public IEnumerable<Monitor> Monitors => AppDbContext.Monitor.Include(p => p.Category);

        public IEnumerable<Monitor> GetFavouriteMonitors => AppDbContext.Monitor.Where(n => n.IsFavourite).Include(p => p.Category);

        public Monitor GetObjectMonitor(int monitorId) => AppDbContext.Monitor.FirstOrDefault(s=> s.Id == monitorId);

        public void Create(Monitor monitor)
        {
            AppDbContext.Monitor.Add(monitor);
        }

        public void Save()
        {
            AppDbContext.SaveChanges();
        }

        public void Update(Monitor monitor)
        {
            AppDbContext.Entry(monitor).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Monitor monitor = AppDbContext.Monitor.Find(id);
            if (monitor != null)
                AppDbContext.Monitor.Remove(monitor);
        }

        public bool MonitorExists(int id)
        {
            return AppDbContext.Monitor.Any(e => e.Id == id);
        }
    }
}
