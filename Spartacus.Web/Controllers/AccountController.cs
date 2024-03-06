using Spartacus.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Join()
        {
            return View();
        }

        public ActionResult UserMenagement()
        {
            tmpModel Abonament = new tmpModel();
            Abonament.Id = "234";
            Abonament.Products = new List<string>{"12 Antrenamente" , "14 zile", "111 lei"};
            return View(Abonament);
        }
    }
}