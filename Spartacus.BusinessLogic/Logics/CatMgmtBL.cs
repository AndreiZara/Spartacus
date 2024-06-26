﻿using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Membership;
using System.Collections.Generic;

namespace Spartacus.BusinessLogic.Logics
{
    public class CatMgmtBL : CatMgmtApi, ICatMgmt
    {
        public bool AddCat(CatTable data) => AddCatAction(data);
        public bool DeleteCatById(int id) => DeleteCatByIdAction(id);
        public CatTable GetCatById(int id) => GetCatByIdAction(id);
        public List<CatTable> GetCats() => GetCatsAction();
        public bool UpdateCat(CatTable data) => UpdateCatAction(data);
    }
}
