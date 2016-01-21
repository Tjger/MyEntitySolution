using System.Web;
using System.Web.Optimization;

namespace EntitySolution.WebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.form-validator.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/jquery.bxslider.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/FontEnd").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-switch.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/Backend").Include(
                  "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-switch.js"));

            bundles.Add(new ScriptBundle("~/bundles/FileUpload").Include(
                 "~/Content/jQueryFileUpload/js/vendor/jquery.ui.widget.js",
                 "~/Content/jQueryFileUpload/js/jquery.iframe-transport.js",
                 "~/Content/jQueryFileUpload/js/jquery.fileupload.js",
                 "~/Content/jQueryFileUpload/js/jquery.fileupload-ui.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
