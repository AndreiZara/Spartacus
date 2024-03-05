using System.Web.Optimization;

namespace Spartacus.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Home style
            bundles.Add(new StyleBundle("~/bundles/main/css").Include(
                      "~/Content/css/style.css", new CssRewriteUrlTransform()));

            // Animate.css
            bundles.Add(new StyleBundle("~/bundles/animate/css").Include(
                      "~/Content/animate.css", new CssRewriteUrlTransform()));

            // Bootstrap style
            bundles.Add(new StyleBundle("~/bundles/bootstrap/css").Include(
                      "~/Content/bootstrap.min.css", new CssRewriteUrlTransform()));

            // Font Awesome icons style
            bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include(
                      "~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));

            // IcoFont style
            bundles.Add(new StyleBundle("~/bundles/icofont/css").Include(
                "~/External/icofont/icofont.min.css", new CssRewriteUrlTransform()));

            // Themify style
            bundles.Add(new StyleBundle("~/bundles/themify/css").Include(
                "~/External/themify/css/themify-icons.css", new CssRewriteUrlTransform()));

            // Magnify style
            bundles.Add(new StyleBundle("~/bundles/magnify/css").Include(
                "~/Content/magnific-popup.css", new CssRewriteUrlTransform()));

            // Slick Carousel style
            bundles.Add(new StyleBundle("~/bundles/Slick/css").Include(
                "~/Content/Slick/slick.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/bundles/cstyle/css").Include(
                "~/Content/css/cstyle.css", new CssRewriteUrlTransform()));

            // Slick Carousel theme style
            bundles.Add(new StyleBundle("~/bundles/Slick/tcss").Include(
                "~/Content/Slick/slick-theme.css", new CssRewriteUrlTransform()));

            // SignIn Style
            bundles.Add(new StyleBundle("~/bundles/Sign-In/css").Include(
                "~/Content/css/Register.css"));

            // Bootstrap
            bundles.Add(new Bundle("~/bundles/bootstrap/js").Include(
                      "~/Scripts/bootstrap.min.js"));
            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include(
                      "~/Scripts/jquery-3.7.1.js"));

            // jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jquery/valid/js").Include(
                      "~/Scripts/jquery.validate.min.js"));

            // Slick Carousel script
            bundles.Add(new ScriptBundle("~/bundles/Slick/js").Include(
                "~/Scripts/Slick/slick.min.js"));

            // Magnific Popup
            bundles.Add(new ScriptBundle("~/bundles/magnific/dist/js").Include(
                "~/Scripts/jquery.magnific-popup.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/js/script.js"));
        }
    }
}