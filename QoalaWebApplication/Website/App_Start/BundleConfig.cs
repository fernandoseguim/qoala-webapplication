using System.Web;
using System.Web.Optimization;

namespace Website
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/vendor/js").
                IncludeDirectory("~/Assets/vendor/", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/vendor/tinymce/js").
                Include(
                    "~/Assets/vendor/tinymce/tinymce.min.js",
                    "~/Assets/vendor/tinymce/jquery.tinymce.min.js"
                )
            );

            bundles.Add(new ScriptBundle("~/assets/js").
                        IncludeDirectory("~/Scripts", "*.js", true));

            bundles.Add(new ScriptBundle("~/assets/js/scrollreveal").
                        Include("~/Scripts/scrollreveal*"));

            bundles.Add(new StyleBundle("~/bundles/vendor/css").
                            IncludeDirectory("~/Assets/vendor/", "*.css", true));

            bundles.Add(new StyleBundle("~/assets/css").
                        IncludeDirectory("~/Content", "*.css", true));

            bundles.Add(new StyleBundle("~/assets/qoala").
                        Include("~/Content/creative.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            
        }
    }
}
