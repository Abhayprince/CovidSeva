using System.Collections.Generic;
using System.Web.Optimization;

namespace CovidSeva
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scripts = new ScriptBundle("~/bundles/js")
                    .Include("~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/covidseva.js");
            scripts.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(scripts);


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                       "~/Scripts/modernizr-*"));

            var styles = new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css");
            styles.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(styles);
        }
    }
    class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files) => files;
    }
}
