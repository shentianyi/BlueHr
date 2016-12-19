using System.Web;
using System.Web.Optimization;

namespace BlueHrWeb
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //    bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-1.11.2.min.js",
            //            "~/Scripts/jquery.ui.widget.js",
            //            "~/Scripts/jquery-ui-1.9.1.custom.min.js"
            //            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-1.9.1.custom.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jquery-plug-in").Include(
                   "~/Scripts/jquery-popModal.js",
                   "~/Scripts/jquery.datetimepicker.full.js",
                   "~/Scripts/jquery.file.upload/jquery.fileupload.js",
                   "~/Scripts/jquery.file.upload/jquery.iframe-transport.js",
                   "~/Scripts/jquery.file.upload/upload.file.data.js",
                //    "~/Scripts/jquery.cookie.js",
                   "~/Scripts/highcharts/highcharts.js"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.slimscroll.js",
                      "~/Scripts/AdminLTE.js",
                      "~/Scripts/jquery.combo.select.js",
                      "~/Scripts/bootstrap.addtabs.js",
                      "~/Scripts/jquery.ztree.all-3.5.js",
                      "~/Scripts/pqgrid.dev.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/go").Include(
                      "~/Scripts/go.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                "~/Scripts/layout.js",
                "~/Scripts/jquery.mCustomScrollbar.concat.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/vue").Include(
                     "~/Scripts/vue.js",
                     "~/Scripts/vue.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/company").Include(
                    "~/Scripts/company.js"));

            bundles.Add(new ScriptBundle("~/bundles/staff").Include(
                    "~/Scripts/staff.js"));

            bundles.Add(new ScriptBundle("~/bundles/certificate").Include(
                "~/Scripts/certificate.js"));

            bundles.Add(new ScriptBundle("~/bundles/magnific-popup").Include(
                "~/Scripts/magnific-popup/jquery.magnific-popup.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-treeview").Include(
              "~/Scripts/bootstrap-treeview-1.2.0/bootstrap-treeview.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/fullcalendar").Include(
                "~/Scripts/fullcalendar-moment.min.js",
                "~/Scripts/fullcalendar.min.js",
                "~/Scripts/fullcalendar-zh-cn.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/multi-select").Include(
                "~/Scripts/jquery.multi-select.js",
                "~/Scripts/jquery.quicksearch.js"
              ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/site.css",
                     "~/Content/bootstrap.css",
                     "~/Content/font-awesome.css",
                     //"~/Content/input-style.css",
                     "~/Content/pagination.css",
                     "~/Content/jquery-popModal.css",
                     "~/Content/jquery.datetimepicker.css",
                     "~/Content/jquery-ui.css",
                     "~/Content/AdminLTE.css",
                     "~/Content/AdminLTEBlue.css",
                     "~/Content/combo.select.css",
                     "~/Content/bootstrap.addtabs.css",
                     "~/Content/jquery-tree.css",
                     "~/Content/pqgrid.dev.css",
                     "~/Content/highcharts.css",
                     "~/Content/fullcalendar.css"
                     ));

            bundles.Add(new StyleBundle("~/Content/layout").Include(
                     "~/Content/layout.css",
                     "~/Content/jquery.mCustomScrollbar.css"
                     ));

            bundles.Add(new StyleBundle("~/Content/company").Include(
                    "~/Content/company.css"
                ));

            bundles.Add(new StyleBundle("~/Content/magnific-popup").Include(
                   "~/Content/magnific-popup.css"
               ));

            bundles.Add(new StyleBundle("~/Content/bootstrap-treeview").Include(
                  "~/Scripts/bootstrap-treeview-1.2.0/bootstrap-treeview.css"
              ));

            bundles.Add(new StyleBundle("~/Content/multi-select").Include(
                 "~/Content/multi-select.css"
             ));
        }
    }
}
