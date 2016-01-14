using System.Web;
using System.Web.Optimization;

namespace ACTS.UI
{
	public class BundleConfig
	{
		// Дополнительные сведения о Bundling см. по адресу http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery")
				.Include("~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap")
				.Include("~/Scripts/bootstrap.js"));

			bundles.Add(new ScriptBundle("~/bundles/admin")
				.Include("~/Scripts/app.js"));

			bundles.Add(new ScriptBundle("~/bundles/tinymce")
				.Include("~/Scripts/tinymce/tinymce.js"));
			
			bundles.Add(new ScriptBundle("~/bundles/datatables")
				.Include("~/Scripts/jquery.dataTables.js")
				.Include("~/Scripts/dataTables.bootstrap.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap-fileinput")
				.Include("~/Scripts/fileinput.js"));

			bundles.Add(new StyleBundle("~/Content/css")
				.Include("~/Content/bootstrap.css",
						"~/Content/font-awesome.css"));

			bundles.Add(new StyleBundle("~/Content/css/admin")
				.Include("~/Content/skins/_all-skins.css",
						"~/Content/AdminLTE.css",
						"~/Content/Admin.css"));

			bundles.Add(new StyleBundle("~/Content/css/datatables")
				.Include("~/Content/dataTables.bootstrap.css"));

			bundles.Add(new StyleBundle("~/Content/css/bootstrap-fileinput")
				.Include("~/Content/bootstrap-fileinput/css/fileinput.css"));
		}
	}
}
				
				