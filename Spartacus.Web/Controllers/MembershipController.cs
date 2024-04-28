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
    public class MembershipController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }
        // GET: Membership
        [HttpPost]
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

                new AdminApi().AddCategory(category);

            }
                return View();

        }


        public ActionResult Update(CategoryTable table)
        {
            var category = new AdminApi().GetCategoryById(table.Id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update (int id, CategoryTable model) 
        {
            var category = new AdminApi().GetParticularCategoryById(id);

            if(category != null)
            {
                category.Title = model.Title;
                category.Description = model.Description;
                category.Price_12 = model.Price_12; 
                category.Price_6 = model.Price_6;   
                category.Price_3 = model.Price_3;   
                category.Price_1 = model.Price_1;
                bool isTrue = new AdminApi().UpdateCategory(category, id);
                return RedirectToAction("Read");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Read() 
        {
            List<CategoryTable> Clist = new List<CategoryTable>();
            Clist = new AdminApi().ReadCategory();
            return View(Clist);
        }

        
        //public ActionResult Delete()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(CategoryTable table)
        //{
        //    var newTable = new AdminApi().GetParticularCategoryById(table.Id);
        //    if(newTable != null) 
        //    {
                
        //    }
        //    return View();
        //}
    }
}