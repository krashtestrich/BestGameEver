using System.Web.Optimization;

namespace GameMvc
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles){

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-2.1.1.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryplugins").Include(
                "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery.cluetip.js",
                        "~/Scripts/jquery.blockUI.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/game").Include(
                        "~/Scripts/Game/PlayerAnimations.js",
                        "~/Scripts/Game/Arena.js",
                        "~/Scripts/Game/Character.js",
                        "~/Scripts/Game/Tournament.js",
                        "~/Scripts/Game/Game.js",
                        "~/Scripts/Game/SkillTree.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
                                                                 "~/Content/Game.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryplugincss").Include(
                "~/Content/jquery.cluetip.css",
                "~/Content/jquery-ui.css",
                "~/Content/jquery-ui.structure.css",
                "~/Content/jquery-ui.theme.css"));
            
        }
    }
}