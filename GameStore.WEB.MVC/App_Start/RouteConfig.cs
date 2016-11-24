using System.Web.Mvc;
using System.Web.Routing;

namespace GameStore.WEB.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("GameDownload", "Game/{gamekey}/download", new {controller = "Game", action = "Download"});

            routes.MapRoute("GameComments", "Game/{gamekey}/comments", new {controller = "Game", action = "Comments"});

            routes.MapRoute("Default", "{controller}/{action}/{key}",
                new {controller = "Home", action = "Index", key = UrlParameter.Optional});
        }
    }
}