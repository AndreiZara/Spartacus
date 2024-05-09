using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System.Collections.Generic;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface ICatMgmt
    {
        bool AddCat(CatTable data);

        List<CatTable> GetCats();

        CatTable GetCatById(int id);

        bool UpdateCat(CatTable data);

        bool DeleteCatById(int id);
    }
}
