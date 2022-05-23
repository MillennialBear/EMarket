using EMarket.Data.Models;
using System.Collections.Generic;

namespace EMarket.Data.Interfaces
{
    public interface IAllMonitors
    {
        IEnumerable<Monitor> Monitors { get; }

        IEnumerable<Monitor> GetFavouriteMonitors { get; }

        Monitor GetObjectMonitor(int monitorId);

        void Create(Monitor monitor);

        void Save();

        void Update(Monitor monitor);

        void Delete(int id);

        bool MonitorExists(int id);
    }
}
