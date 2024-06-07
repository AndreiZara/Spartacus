using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Location;
using System.Collections.Generic;
using System.Linq;

namespace Spartacus.BusinessLogic.Core
{
    public class LocMgmtApi
    {
        internal bool AddLocAction(LTable data)
        {
            using var debil = new GymContext();
            var loc = debil.Locations.FirstOrDefault(l => l.Name == data.Name);
            if (loc != null) return false;

            debil.Locations.Add(data);
            debil.SaveChanges();

            return true;
        }

        internal List<LTable> GetLocsAction()
        {
            using var debil = new GymContext();
            var locs = debil.Locations.ToList();
            return locs;
        }

        internal LTable GetLocByIdAction(int id)
        {
            using var debil = new GymContext();
            var loc = debil.Locations.FirstOrDefault(l => l.Id == id);
            return loc;
        }

        internal bool UpdateLocAction(LTable data)
        {
            using var debil = new GymContext();
            var loc = debil.Locations.FirstOrDefault(l => l.Id == data.Id);
            if (loc == null) return false;

            if (data.Name != loc.Name)
            {
                var exists = debil.Locations.FirstOrDefault(l => l.Name == data.Name) != null;
                if (exists) return false;
             
                loc.Name = data.Name;
            }
            loc.Capacity = data.Capacity;
            loc.Address = data.Address;
            loc.LastUpdate = data.LastUpdate;

            debil.SaveChanges();
            return true;
        }

        internal bool DeleteLocByIdAction(int id)
        {
            using (var debil = new GymContext())
            {
                var loc = debil.Locations.Find(id);
                if (loc == null) return false;
                debil.Locations.Remove(loc);
                debil.SaveChanges();
            }
            return true;
        }
    }
}
