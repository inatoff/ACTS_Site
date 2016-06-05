using System.Web;
using System.Web.Optimization;

namespace ACTS.UI
{
	public class BundleConfig
	{
		// Дополнительные сведения о Bundling см. по адресу http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.UseCdn = true;

			#region Scripts

			bundles.Add(new ScriptBundle("~/bundles/jquery")
				.Include("~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval-admin")
				.Include("~/Scripts/jquery.validate.js")
				.Include("~/Scripts/jquery.validate.unobtrusive.js")
				.Include("~/Scripts/Admin/validationOptions.js"));

			bundles.Add(new ScriptBundle("~/bundels/jqery-unobtrusive-ajax")
				.Include("~/Scripts/jquery.unobtrusive-ajax.js"));

			bundles.Add(new ScriptBundle("~/bundles/expressive")
				.Include("~/Scripts/expressive.annotations.validate.js"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap")
				.Include("~/Scripts/bootstrap.js"));

			bundles.Add(new ScriptBundle("~/bundles/admin")
				//.Include("~/Scripts/jquery.slimscroll.js")
				.Include("~/Scripts/app.js"));

			bundles.Add(new ScriptBundle("~/bundles/main-layout")
				.Include("~/Scripts/actsScripts/fixedMenu.js")
				.Include("~/Scripts/actsScripts/Slider.js"));

			bundles.Add(new ScriptBundle("~/bundles/personalPage")
				.Include("~/Scripts/actsScripts/personalPageScripts.js"));

			bundles.Add(new ScriptBundle("~/bundles/tinymce")
				.Include("~/Scripts/tinymce/tinymce.js")
				.Include("~/Scripts/Admin/initTinyEditor.js"));

			bundles.Add(new ScriptBundle("~/bundles/tinymce-uk")
				.Include("~/Scripts/tinymce/tinymce.js")
				.Include("~/Scripts/Admin/initTinyEditor_uk.js"));

			bundles.Add(new ScriptBundle("~/bundles/datatables")
				.Include("~/Scripts/jquery.dataTables.js")
				.Include("~/Scripts/dataTables.bootstrap.js")
				.Include("~/Scripts/Admin/initDataTable.js"));

			bundles.Add(new ScriptBundle("~/bundles/datatables-uk")
				.Include("~/Scripts/jquery.dataTables.js")
				.Include("~/Scripts/dataTables.bootstrap.js")
				.Include("~/Scripts/Admin/initDataTable_uk.js"));

			bundles.Add(new ScriptBundle("~/bundles/fileinput")
				.Include("~/Scripts/fileinput.js")
				.Include("~/Scripts/Admin/fileinputOptions.js"));

			bundles.Add(new ScriptBundle("~/bundles/fileinput-uk")
				.Include("~/Scripts/fileinput.js")
				.Include("~/Scripts/Admin/fileinputOptions_uk.js")
				.Include("~/Scripts/fileinput_locale_uk.js"));

			bundles.Add(new ScriptBundle("~/bundles/select2")
				.Include("~/Scripts/select2.js")
				.Include("~/Scripts/Admin/initSelect2.js"));

			bundles.Add(new ScriptBundle("~/bundles/iCheck")
				.Include("~/Scripts/jquery.icheck.js")
				.Include("~/Scripts/Admin/init_iCheck.js"));

			bundles.Add(new ScriptBundle("~/bundles/daterange")
				.Include("~/Scripts/moment.js")
				.Include("~/Scripts/daterangepicker.js")
				.Include("~/Scripts/Admin/initDateRange.js"));

			bundles.Add(new ScriptBundle("~/bundles/daterange-uk")
				.Include("~/Scripts/moment-with-locales.js")
				.Include("~/Scripts/daterangepicker.js")
				.Include("~/Scripts/Admin/initDateRange_uk.js"));
			
			bundles.Add(new ScriptBundle("~/bundles/sortable", 
				"https://cdnjs.cloudflare.com/ajax/libs/Sortable/1.4.2/Sortable.js")
				.Include("~/Scripts/Sortable.js"));

			bundles.Add(new ScriptBundle("~/bundles/teachingStaffWrapper")
				.Include("~/Scripts/actsScripts/teachingStaffWrapper.js"));

			#endregion

			#region Styles

			bundles.Add(new StyleBundle("~/Content/css")
				.Include("~/Content/bootstrap.css",
						"~/Content/font-awesome.css"));

			bundles.Add(new StyleBundle("~/Content/css/personal")
				.Include("~/Content/css/PersonalPageLayoutStyles.css"));

			bundles.Add(new StyleBundle("~/Content/css/admin")
				.Include("~/Content/skins/_all-skins.css",
						"~/Content/AdminLTE.css",
						"~/Content/Admin.css"));

			bundles.Add(new StyleBundle("~/Content/css/datatables")
				.Include("~/Content/dataTables.bootstrap.css"));

			bundles.Add(new StyleBundle("~/Content/css/bootstrap-fileinput")
				.Include("~/Content/bootstrap-fileinput/css/fileinput.css"));

			bundles.Add(new StyleBundle("~/Content/css/select2")
				.Include("~/Content/css/select2.css"));

			bundles.Add(new StyleBundle("~/Content/css/iCheck/blue")
				.Include("~/Content/iCheck/square/blue.css"));

			bundles.Add(new StyleBundle("~/Content/css/daterange")
				.Include("~/Content/daterangepicker.css"));

			bundles.Add(new StyleBundle("~/Content/css/MainPage")
				.Include("~/Content/css/Site.css",
						 "~/Content/css/MainPage.css"));
			#endregion
		}
	}
}
				
				