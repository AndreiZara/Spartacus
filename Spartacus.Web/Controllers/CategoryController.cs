using Microsoft.Ajax.Utilities;
using Spartacus.BusinessLogic.Core;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Spartacus.Web.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Read()
        {
            var cats = new AdminApi().GetCategories();
            return View(cats);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(CatTable table)
        {
            if (ModelState.IsValid) 
            {
                CatTable category = new CatTable
                {
                    Id = table.Id,
                    Title = table.Title,
                    Description = table.Description,
                    PriceOneYear = table.PriceOneYear,
                    PriceSixMonths = table.PriceSixMonths,
                    PriceThreeMonths = table.PriceThreeMonths,
                    PriceOneMonth = table.PriceOneMonth,

                };

                new AdminApi().AddCategory(category);

            }
                return View();

        }


        public ActionResult Update(int id)
        {
            var cat = new AdminApi().GetCategoryById(id);
            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CatTable data) 
        {
            if (ModelState.IsValid)
            {
                var catUpdated = new AdminApi().UpdateCategory(data);

                if (catUpdated)
                    return RedirectToAction("Read");
                else
                    ModelState.AddModelError("UpdateMessage", "Update failed!");
            }
            return View(data);
        }
    }
}