using System.Web.Mvc;

namespace GameWebMVC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            /*context.MapRoute(
                "Admin_groups",
                "Admin/Groups/{action}/{id}",
                new { controller = "Groups", action = "Index", id = UrlParameter.Optional },
                new [] { "GameWebMVC.Areas.Admin.Controllers" }
            );*/

            context.MapRoute(
                name: "Admin_default",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { controller = "Groups", action = "Index", id = UrlParameter.Optional },
                namespaces: new [] { "GameWebMVC.Areas.Admin.Controllers" }
            );
        }
    }
}