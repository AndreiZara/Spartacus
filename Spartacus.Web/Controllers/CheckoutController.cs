using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Enums;
using Spartacus.Web.Models;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class CheckoutController : BaseController
    {
        private readonly IMain _main;
        public CheckoutController()
        {
            _main = BussinesLogic.GetMainBL();
        }

        public ActionResult Index(int mid, MsDuration dur)
        {
            var isLoggedIn = SessionStatus();
            if (!isLoggedIn) return RedirectToAction("Login", "Account");

            var cat = _main.GetCatById(mid);
            if (cat == null) return HttpNotFound();

            var durationSet = _session.SetMsDurationFor(Request.Cookies["UserCookie"].Value, dur);
            if (!durationSet) return HttpNotFound();

            int price = 0;

            switch (dur)
            {
                case MsDuration.OneMonth:
                    price = cat.PriceOneMonth;
                    break;
                case MsDuration.ThreeMonths:
                    price = cat.PriceThreeMonths;
                    break;
                case MsDuration.SixMonths:
                    price = cat.PriceSixMonths;
                    break;
                case MsDuration.OneYear:
                    price = cat.PriceOneYear;
                    break;
            }

            var check = new Checkout
            {
                Price = price,
                //Duration = dur,
                EndTime = DateTime.Now.AddMonths((int)dur)
            };

            return View(check);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Checkout check)
        {
            if (ModelState.IsValid)
            {
                var dates = check.Expiry.Split('/');
                var month = int.Parse(dates[0]);
                var year = int.Parse(dates[1]);

                DateTime expDate = new DateTime(CultureInfo.InvariantCulture.Calendar.ToFourDigitYear(year), month, 1);
                if (expDate.Year > DateTime.Now.Year && expDate.Month > DateTime.Now.Month)
                {
                    var membershipCreated = _session.AddMembershipFor(Session["Username"] as string, _session.GetMsDurationFor(Request.Cookies["UserCookie"].Value));
                    if (membershipCreated)
                        return RedirectToAction("Profile", "Account");
                    else
                        ModelState.AddModelError("CheckoutMessage", "Purchase failed!");
                }
                else
                {
                    ModelState.AddModelError("Expiry", "Card expired!");
                }

            }
            return View(check);
        }
    }
}