using System.Web;
using System.Web.Optimization;

namespace SerwisOgloszen
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                           "~/Scripts/jquery.validate*",
                           "~/Scripts/bootstrap.js",
                           "~/Scripts/autosize.js",
                           "~/Scripts/sweetalert2.js",
                            "~/Scripts/jquery.cookie.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/sweetalert2.css",
                      "~/Content/site.css"));
        }
    }
}
