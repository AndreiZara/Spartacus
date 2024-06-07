using Spartacus.Domain.Entities.Location;
using System.Collections.Generic;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface ILocMgmt
    {
        bool AddLoc(LTable data);

        List<LTable> GetLocs();

        LTable GetLocById(int id);

        bool UpdateLoc(LTable data);

        bool DeleteLocById(int id);
    }
}
