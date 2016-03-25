using System.Web.Mvc;

namespace ACTS.UI.Areas.Peoples
{
    public class PeoplesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Peoples";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "ToDefaultPeoplesArea",
                url: "PersonalPage/{nameSlug}/{action}",
                defaults: new { controller = "PersonalPage", action = "Index"});

            context.MapRoute(
                name: "TeachersBlog",
                url: "Teacher/{nameSlug}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional });


            context.MapRoute(
                name: "Peoples_default",
                url: "Profile/{action}/{id}",
                defaults: new { controller= "Profile", action = "Index", id = UrlParameter.Optional });
        }
    }
}