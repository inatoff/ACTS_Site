using System.Web.Mvc;

namespace ACTS.UI.Areas.Blog
{
    public class BlogAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Blog";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Blog_default",
                "Blog/{nameSlug}/{action}",
                new { controller = "Blog", action = "Blog" }
            );
        }
    }
}