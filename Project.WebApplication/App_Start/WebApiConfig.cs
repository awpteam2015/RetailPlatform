using System.Web.Http;


namespace Project.WebApplication
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //  config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "{area}/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //  GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector),
            //                                         new NewHttpControllerSelector(
            //                                             GlobalConfiguration.Configuration));


        }
    }
}