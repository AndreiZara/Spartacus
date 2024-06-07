using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Location;
using System.Collections.Generic;

namespace Spartacus.BusinessLogic.Logics
{
    public class LocMgmtBL : LocMgmtApi, ILocMgmt
    {
        public bool AddLoc(LTable data) => AddLocAction(data);
        public bool DeleteLocById(int id) => DeleteLocByIdAction(id);
        public LTable GetLocById(int id) => GetLocByIdAction(id);
        public List<LTable> GetLocs() => GetLocsAction();
        public bool UpdateLoc(LTable data) => UpdateLocAction(data);
    }
}
