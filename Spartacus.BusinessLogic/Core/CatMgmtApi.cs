using Spartacus.BusinessLogic.DBContext;
using Spartacus.Domain.Entities.Membership;
using System.Collections.Generic;
using System.Linq;

namespace Spartacus.BusinessLogic.Core
{
    public class CatMgmtApi
    {
        internal bool AddCatAction(CatTable data)
        {
            using (var debil = new GymContext())
            {
                var cat = debil.Categories.FirstOrDefault(c => c.Title == data.Title);
                if (cat != null) return false;

                debil.Categories.Add(data);
                debil.SaveChanges();
            }
            return true;
        }

        internal List<CatTable> GetCatsAction()
        {
            using var debil = new GymContext();
            var cats = debil.Categories.ToList();
            return cats;
        }

        internal CatTable GetCatByIdAction(int id)
        {
            using var debil = new GymContext();
            var cat = debil.Categories.FirstOrDefault(c => c.Id == id);
            return cat;
        }

        internal bool UpdateCatAction(CatTable data)
        {
            using (var debil = new GymContext())
            {
                var cat = debil.Categories.FirstOrDefault(x => x.Id == data.Id);

                if (cat == null) return false;

                if (data.Title != cat.Title)
                {
                    var exists = debil.Categories.FirstOrDefault(l => l.Title == data.Title) != null;
                    if (exists) return false;

                    cat.Title = data.Title;
                }
                cat.Description = data.Description;
                cat.PriceOneYear = data.PriceOneYear;
                cat.PriceSixMonths = data.PriceSixMonths;
                cat.PriceThreeMonths = data.PriceThreeMonths;
                cat.PriceOneMonth = data.PriceOneMonth;

                debil.SaveChanges();
            }
            return true;
        }

        internal bool DeleteCatByIdAction(int id)
        {
            using (var debil = new GymContext())
            {
                var cat = debil.Categories.Find(id);
                if (cat == null) return false;
                debil.Categories.Remove(cat);
                debil.SaveChanges();
            }
            return true;
        }
    }
}
