using System.Web;
using System.Web.Optimization;

namespace LEISIO
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/vendor").Include(
                      "~/vendors/bootstrap/dist/css/bootstrap.min.css",
                      "~/vendors/font-awesome/css/font-awesome.min.css",
                      "~/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                      "~/vendors/bootstrap-daterangepicker/daterangepicker.css",
                      "~/vendors/datatables.net-bs/css/dataTables.bootstrap.min.css",
                      "~/vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css",
                      "~/vendors/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css",
                      "~/vendors/datatables.net-responsive-bs/css/responsive.bootstrap.min.css",
                      "~/vendors/datatables.net-scroller-bs/css/scroller.bootstrap.min.css",
                      "~/build/css/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                      "~/vendors/jquery/dist/jquery.min.js",
                      "~/vendors/bootstrap/dist/js/bootstrap.min.js",
                      "~/vendors/Chart.js/dist/Chart.min.js",
                      "~/vendors/moment/min/moment.min.js",
                      "~/vendors/bootstrap-daterangepicker/daterangepicker.js",
                      "~/vendors/echarts/dist/echarts.min.js",
                      "~/vendors/echarts/map/js/world.js",
                      "~/vendors/datatables.net/js/jquery.dataTables.min.js",
                      "~/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js",
                      "~/vendors/datatables.net-buttons/js/dataTables.buttons.min.js",
                      "~/vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                      "~/vendors/datatables.net-buttons/js/buttons.flash.min.js",
                      "~/vendors/datatables.net-buttons/js/buttons.html5.min.js",
                      "~/vendors/datatables.net-buttons/js/buttons.print.min.js",
                      "~/vendors/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
                      "~/vendors/datatables.net-keytable/js/dataTables.keyTable.min.js",
                      "~/vendors/datatables.net-responsive/js/dataTables.responsive.min.js",
                      "~/vendors/datatables.net-responsive-bs/js/responsive.bootstrap.js",
                      "~/vendors/datatables.net-scroller/js/dataTables.scroller.min.js",
                      "~/vendors/jszip/dist/jszip.min.js",
                      "~/vendors/pdfmake/build/pdfmake.min.js",
                      "~/vendors/pdfmake/build/vfs_fonts.js",
                      "~/build/js/custom.js",
                      "~/build/js/my/geral.js",
                      "~/vendors/fastclick/lib/fastclick.js",
                      "~/vendors/nprogress/nprogress.js",
                      "~/vendors/iCheck/icheck.min.js"));
        }
    }
}
