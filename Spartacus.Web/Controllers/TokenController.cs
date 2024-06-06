using Spartacus.BusinessLogic.Interfaces;
using Spartacus.BusinessLogic;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spartacus.Web.ActionFilters;
using Spartacus.Domain.Entities.Tokens;

namespace Spartacus.Web.Controllers
{
    public class TokenController : Controller
    {
        private readonly ISession _session;
        private readonly IMain _sessionMain;
        private readonly IAdmin _sessionAdmin;

        public TokenController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
            var Mbl = new BussinesLogic();
            _sessionMain = Mbl.GetMainBL();
            var Abl = new BussinesLogic();
            _sessionAdmin = Abl.GetAdminBL();
        }

        [HttpGet]
        public ActionResult ReadResToken()
        {
            List<ResetToken> Clist = new List<ResetToken>();
            Clist = _sessionMain.GetTokenList();
            return View(Clist);
        }

        [HttpPost]
        public ActionResult DeleteResToken()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteResToken(int Id)
        {
            bool isTrue = _sessionMain.DeleteToken(Id); 
            if (isTrue) { return RedirectToAction("ReadReadResToken"); }
            return View();
        }

        [HttpGet]
        public ActionResult ReadRegToken()
        {
            List<RegisterToken> Clist = new List<RegisterToken>();
            Clist = _sessionMain.GetRegTokenList();
            return View(Clist);
        }

        
        public ActionResult DeleteRegToken(int Id)
        {
            var token = _sessionMain.GetRegTokenById(Id);
            return View(token);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRegToken(RegisterToken token)
        {
            bool isTrue = _sessionMain.DeleteRegToken(token.Id);
            if (isTrue) { return RedirectToAction("ReadRegToken"); }
            return View();
        }
    }
}