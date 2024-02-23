using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace Spartacus.Web.App_Start
{
     public class BundleConfig
     {
          public static void RegisterBundles(BundleCollection bundles)
          {
               // Home style
               bundles.Add(new StyleBundle("~/bundles/main/css").Include(
                         "~/Content/style.css", new CssRewriteUrlTransform()));

               // Animate.css
               bundles.Add(new StyleBundle("~/bundles/animate/css").Include(
                         "~/Content/animate.min.css"));

               // Bootstrap style
               bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                         "~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));

               // Font Awesome icons style
               bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include(
                         "~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));

               // Bootstrap
               bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
                         "~/Scripts/bootstrap.min.js"));
               // jQuery
               bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include(
                         "~/Scripts/jquery-3.3.1.min.js"));


               // jQuery Validation
               bundles.Add(new ScriptBundle("~/bundles/validation/js").Include(
                         "~/Scripts/jquery.validate.min.js"));


          }
     }
}