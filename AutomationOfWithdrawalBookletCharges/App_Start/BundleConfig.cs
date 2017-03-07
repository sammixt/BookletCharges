using System.Web;
using System.Web.Optimization;

namespace AutomationOfWithdrawalBookletCharges
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                 "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/flotchart").Include(
                        "~/Scripts/flot/jquery.flot.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                        "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable1").Include(
                        "~/Scripts/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable2").Include(
                       "~/Scripts/dataTables.bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable3").Include(
                       "~/Scripts/dataTables.buttons.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable4").Include(
                       "~/Scripts/jszip.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable5").Include(
                       "~/Scripts/pdfmake.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable6").Include(
                       "~/Scripts/vfs_fonts.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatable7").Include(
                       "~/Scripts/buttons.html5.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/select").Include(
                        "~/Scripts/bootstrap-select*"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/dataTables.bootstrap.min"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/countreq").Include(
                       "~/Scripts/CountRequest.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/dataTables.bootstrap.min.css",
                      "~/Content/buttons.dataTables.min.css",
                      "~/Content/bootstrap-select.css",
                      "~/Content/bootstrap-datepicker3.css",
                      "~/Content/site.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
