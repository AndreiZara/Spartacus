using Spartacus.Domain.Entities.Membership;
using System.Collections.Generic;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IMain
    {
        List<CatTable> GetCats();
        CatTable GetCatById(int id);
    }
}
