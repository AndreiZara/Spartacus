using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Membership;
using System.Collections.Generic;

namespace Spartacus.BusinessLogic.Logics
{
    public class MainBL : MainApi, IMain
    {
        //public CatTable GetCatByHash(byte[] hash) => GetCatByHashAction(hash);

        public CatTable GetCatById(int id) => GetCatByIdAction(id);

        public List<CatTable> GetCats() => GetCatsAction();
    }
}
