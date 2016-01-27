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
                "Peoples_default",
                "Peoples/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}