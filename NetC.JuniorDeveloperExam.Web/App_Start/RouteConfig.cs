using System.Web.Mvc;
using System.Web.Routing;

namespace NetC.JuniorDeveloperExam.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
            );

            // 404 Routing
            routes.MapRoute(
                name: "404-PageNotFound",
                // Handels any non-existing urls
                url: "{*url}",
                //Custom errors
                defaults: new { controller = "Error", action = "NotFound" }
            );
        }
    }
}
