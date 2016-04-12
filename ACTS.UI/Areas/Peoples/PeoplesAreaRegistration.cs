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
                name: "TeachersBlog",
                url: "Teacher/{nameSlug}/{action}/{id}",
                defaults: new { controller = "Blog", action = "PersonalPage", id = UrlParameter.Optional });


            context.MapRoute(
                name: "Peoples_default",
                url: "Profile/{action}/{id}",
                defaults: new { controller= "Profile", action = "Index", id = UrlParameter.Optional });
        }
    }
}