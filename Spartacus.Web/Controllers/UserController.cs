using Spartacus.BusinessLogic.Core;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace Spartacus.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {

        public ActionResult UCreate()
        {
            return View();
        }


        // GET: User
        [HttpPost]
        public ActionResult UCreate(UDbTable login)
        public ActionResult Read()
        {

            if (ModelState.IsValid)
            {
                UDbTable data = new UDbTable
                {
                    Username = login.Username,
                    Id= login.Id,
                    Password = login.Password,
                    Email = login.Email,
                    LastLogin = DateTime.Now,
                    LasIp = login.LasIp,
                    Level = login.Level,
                };
            var users = new AdminApi().GetUsersAction();
            return View(users);
        }

        public ActionResult Delete(int id)
        {
            var user = new AdminApi().GetUserByIdAction(id);
            if (user == null) return HttpNotFound();
            return View(user);

                Session["Id"] = data.Id;
                Session["Username"] = data.Username;
                Session["Password"] = data.Password;
                Session["Email"] = data.Email;
                Session["LastLogin"] = data.LastLogin;
                Session["LastIp"] = data.LasIp;
                
                AdminApi api = new AdminApi();
                api.AddUser(data);
                
                
            }
            return View(login);

        }

        
        public ActionResult Update(UDbTable login)
        {


            if (ModelState.IsValid)
            {
                UDbTable data = new UDbTable
                {
                    Username = login.Username,
                    Id = login.Id,
                    Password = login.Password,
                    Email = login.Email,
                    LastLogin = DateTime.Now,
                    LasIp = login.LasIp,
                    Level = login.Level,
                };
                
                bool isTrue = new AdminApi().UpdateUser(data,login.Id);

                if (isTrue)
                {
                    return RedirectToAction("Read");
                }

                else { return View(); }
            }
            Session["Id"] = login.Id;

            return View();
        }


        [HttpGet]
        public ActionResult URead() 
        {
            List<UDbTable> Ulist = new List<UDbTable>();
            UDbTable newTable = new UDbTable();
            AdminApi api = new AdminApi();
            Ulist = api.ReadUser();
            newTable = Ulist[3];

            return View(newTable);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = new AdminApi().DeleteUserByIdAction(id);
            if (result == false) return HttpNotFound();
            return RedirectToAction("Read");
        }
    }
}