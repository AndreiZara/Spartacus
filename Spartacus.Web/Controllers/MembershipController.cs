using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Web.ActionFilters;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IAdmin _admin;

        public MembershipController()
        {
            var bl = new BussinesLogic();
            _admin = bl.GetAdminBL();
        }

        [AdminMod(Domain.Enums.URole.Admin)]
        public ActionResult Create()
        {
            
            return View();
        }

        // GET: Membership
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryTable table)
        {
            if (ModelState.IsValid) 
            {
                CategoryTable category = new CategoryTable
                {
                    Id = table.Id,
                    Title = table.Title,
                    Description = table.Description,
                    Price_12 = table.Price_12,
                    Price_6 = table.Price_6,
                    Price_3 = table.Price_3,
                    Price_1 = table.Price_1,

                };

                _admin.AddCategory(category);

            }
                return View();

        }

        [AdminMod(Domain.Enums.URole.Admin, Domain.Enums.URole.Moderator)]
        public ActionResult Update(CategoryTable table)
        {
            var category = _admin.GetCategoryById(table.Id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update (int id, CategoryTable model) 
        {
            var category = _admin.GetParticularCategoryById(id);

            if(category != null)
            {
                category.Title = model.Title;
                category.Description = model.Description;
                category.Price_12 = model.Price_12; 
                category.Price_6 = model.Price_6;   
                category.Price_3 = model.Price_3;   
                category.Price_1 = model.Price_1;
                bool isTrue = _admin.UpdateCategory(category, id);
                return RedirectToAction("Read");
            }
            return View();
        }

        [AdminMod(Domain.Enums.URole.Admin, Domain.Enums.URole.Moderator)]
        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Read() 
        {
            List<CategoryTable> Clist = new List<CategoryTable>();
            Clist = _admin.ReadCategory();
            return View(Clist);
        }

        
        
    }
}