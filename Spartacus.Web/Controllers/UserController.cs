using Spartacus.BusinessLogic.Core;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        public ActionResult Read()
        {
            var users = new AdminApi().GetUsersAction();
            return View(users);
        }

        public ActionResult Delete(int id)
        {
            var user = new AdminApi().GetUserByIdAction(id);
            if (user == null) return HttpNotFound();
            return View(user);
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