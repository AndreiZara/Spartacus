using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Membership;
using System.Collections.Generic;
using System.Linq;

namespace Spartacus.BusinessLogic.Core
{
    public class MainApi
    {
        public List<CatTable> GetCatsAction()
        {
            List<CatTable> cats;
            using (var debil = new CategoryContext())
            {
                cats = debil.Categories.ToList();
            }
            return cats;
        }

        //public CatTable GetCatByHashAction(byte[] hash)
        //{
        //    CatTable cat;
        //    using (var debil = new CategoryContext())
        //    using (var service = new SHA256CryptoServiceProvider())
        //    {
        //        cat = debil.Categories.FirstOrDefault(c => service.ComputeHash(Encoding.UTF8.GetBytes(c.Title)) == hash);
        //    }
        //    return cat;
        //}

        public CatTable GetCatByIdAction(int id)
        {
            CatTable cat;
            using (var debil = new CategoryContext())
            {
                cat = debil.Categories.FirstOrDefault(c => c.Id == id);
            }
            return cat;
        }
    }
}
