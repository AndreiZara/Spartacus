using Spartacus.BusinessLogic.Interfaces;
using Spartacus.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Web.Extension;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.BusinessLogic;
using Spartacus.Web.Extension;

{
    public class BaseController : Controller
    {

        {
        {
                {
                    System.Web.HttpContext.Current.SetMySessionObject(profile);
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                }
                else
                { 
                    Session.Abandon();
                    EatCookie();
                }
            }
            else
                {
                System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
            }
        }

        protected void EatCookie()
        {
                    System.Web.HttpContext.Current.Session.Clear();
                    {
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                            ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                        }
                    }

                    System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
            }
        }
    }
}