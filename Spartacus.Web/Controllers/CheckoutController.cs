using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Enums;
using Spartacus.Web.Filters;
using Spartacus.Web.Models;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    [Confirmed]
    public class CheckoutController : BaseController
    {
        private readonly ICatMgmt _catMgmt = BussinesLogic.GetCatMgmtBL();
        private readonly ILocMgmt _locMgmt = BussinesLogic.GetLocMgmtBL();

        public ActionResult Begin(int? cid, MsDuration? dur)
        {
            SessionStatus();
            if (cid == null || dur == null) return HttpNotFound();

            var isLoggedIn = SessionStatus();
            if (!isLoggedIn) return RedirectToAction("Login", "Account", new { returnUrl = Request.Url.PathAndQuery });

            var cat = _catMgmt.GetCatById(cid.Value);
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
                Locations = new SelectList(_locMgmt.GetLocs(), "Id", "Name")
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
                    var membershipCreated = _session.AddMembershipFor(user.Username, new MsData
                    {
                        CatId = cid,
                        Period = dur,
                        LocId = check.LocId
                    });
                    switch(membershipCreated)
                    {
                        case AddMemResp.Failed: TempData["ErrorMessage"] = "Payment failed!"; break;
                        
                        case AddMemResp.FullCapacity: ModelState.AddModelError("LocId", "Selected location is full."); break;
                        
                        case AddMemResp.Success: return RedirectToAction("Index", "Account");
                    
                        default: throw new InvalidOperationException();                     
                    }
                }
                else
                    ModelState.AddModelError("Expiry", "Card expired!");
            }

            TempData["Period"] = dur;
            TempData["CategoryId"] = cid;
            check.Locations = new SelectList(_locMgmt.GetLocs(), "Id", "Name");

            return View(check);
        }
    }
}