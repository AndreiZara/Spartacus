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
        private readonly ICatMgmt _mgmt = BussinesLogic.GetCatMgmtBL();
        
        public ActionResult Begin(int? cid, MsDuration? dur)
        {
            SessionStatus();
            if (cid == null || dur == null) return HttpNotFound();

            var isLoggedIn = SessionStatus();
            if (!isLoggedIn) return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.PathAndQuery });

            var cat = _mgmt.GetCatById(cid.Value);
            if (cat == null) return HttpNotFound();

            int price = dur switch
            {
                MsDuration.OneMonth => cat.PriceOneMonth,
                MsDuration.ThreeMonths => cat.PriceThreeMonths,
                MsDuration.SixMonths => cat.PriceSixMonths,
                MsDuration.OneYear => cat.PriceOneYear,
                _ => throw new InvalidOperationException()
            };

            TempData["Period"] = dur;
            TempData["CategoryId"] = cat.Id;

            return View(new Checkout
            {
                Price = price,
                EndTime = DateTime.Now.AddMonths((int)dur),
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Begin(Checkout check)
        {
            var cid = (int?)TempData["CategoryId"];
            var dur = (MsDuration?)TempData["Period"];

            if (ModelState.IsValid)
            {
                var dates = check.Expiry.Split('/');
                int month, year;
                try
                {
                    month = int.Parse(dates[0]);
                    year = int.Parse(dates[1]);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return RedirectToAction("Begin", new { cid, dur });
                }

                DateTime expDate = new DateTime(CultureInfo.InvariantCulture.Calendar.ToFourDigitYear(year), month, 1);
                // get last day of the month
                expDate = expDate.AddMonths(1).AddDays(-1);
                
                if (expDate > DateTime.Now)
                {
                    var user = _session.GetUserByCookie(Request.Cookies["UserCookie"].Value);
                    var membershipCreated = _session.AddMembershipFor(user.Username, cid, dur);
                    if (membershipCreated)
                        return RedirectToAction("Index", "Account");
                    else
                        TempData["ErrorMessage"] = "Payment failed";
                }
                else
                    ModelState.AddModelError("Expiry", "Card expired!");
            }
            return RedirectToAction("Begin", new { cid, dur });
        }
    }
}