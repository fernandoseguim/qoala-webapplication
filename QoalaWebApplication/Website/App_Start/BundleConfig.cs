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
                        IncludeDirectory("~/Assets/javascript/", "*.js", true));

            bundles.Add(new StyleBundle("~/bundles/vendor/css").
                            IncludeDirectory("~/Assets/vendor/", "*.css", true));

            bundles.Add(new StyleBundle("~/assets/css").
                        IncludeDirectory("~/Assets/css/", "*.css", true));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

        }
    }
}
