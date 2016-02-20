using System.Web.Mvc;
using System.Web.Routing;

namespace Bm
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Login", "login", new { area = "Base", controller = "Session", action = "Login", token = UrlParameter.Optional });

            routes.MapRoute("Logout", "logout", new { area = "Base", controller = "Session", action = "Logout", token = UrlParameter.Optional });

            routes.MapRoute(
                name: "DefaultRoot",
                url: "",
                defaults: new { area = "Base", controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { area = "Base", controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
