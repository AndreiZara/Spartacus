using Spartacus.BusinessLogic.Interfaces;
using Spartacus.BusinessLogic;
using Spartacus.Domain.Entities.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
using System.Web.UI.WebControls;
using Spartacus.Web.ActionFilters;

namespace Spartacus.Web.Controllers
{
    public class ServiceController : Controller
    {

        private readonly IMain _main;
        private readonly IAdmin _admin;

        public ServiceController()
        {
            var bl = new BussinesLogic();
            _admin = bl.GetAdminBL();
            var main = new BussinesLogic();
            _main = main.GetMainBL();
        }

        [AdminMod(Domain.Enums.URole.Admin)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SerTable login) 
        {
            if (ModelState.IsValid)
            {
                //string filename = service.File.FileName;
                SerTable data = new SerTable
                {
                    Title = login.Title,
                    Description = login.Description,
                    File = login.File,
                    FileName = "salutlime"
                };

                if (_admin.GetDetailByActivity(login.Title) != null)
                {
                    MenDetTable table = _admin.GetDetailByActivity(login.Title);
                    table.ServiceId = login.ServiceId;

                    _admin.UpdateDetail(table, table.Id);

                    data.MenDetTables.Add(table);

                    _admin.AddService(login);

                    return View(data);
                }

                _admin.AddService(login);
                return View(data);
            }
            return View();
        }
    }
}