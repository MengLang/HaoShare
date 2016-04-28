using System.Web;
using System.Web.Optimization;

namespace HaoShare
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Content/js.js").Include(
                "~/Content/js/H5lock.js",
                "~/Content/js/jquery-1.10.2.js",
                "~/Content/js/ndoo.js",
                "~/Content/js/jquery.mobile-1.4.5.js",
                "~/Content/js/slick.js"
                ));

            bundles.Add(new StyleBundle("~/Content/css.css").Include(
                 "~/Content/css/slick.css",
                "~/Content/css/jquery.mobile-1.4.5.css",
                "~/Content/css/style.css"
                ));
        }
    }
}
