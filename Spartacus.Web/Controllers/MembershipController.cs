using Spartacus.BusinessLogic.Core;
using Spartacus.Domain.Entities.Membership;
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
        public ActionResult Create(CatTable tabel)
        {
            if (ModelState.IsValid) 
            {
                CatTable category = new CatTable
                {
                    Id = tabel.Id,
                    Title = tabel.Title,
                    Description = tabel.Description,
                };

                new AdminApi().AddCategory(category);
            }

                return View();
        }
    }
}