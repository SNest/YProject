namespace YFit.Web
{
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi", 
                routeTemplate: "api/{controller}/{id}", 
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "PointsFrames",
                routeTemplate: "api/PointsFrames/{id}",
                defaults: new { controller = "PointsFrames", id = RouteParameter.Optional });
        }
    }
}