using BEServerManager.Util;
using Owin;
using System.Web.Http;

namespace BEServerManager.WebApi.App_Start
{
    class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
           

            // Configure Web API for Self-Host
            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                name: "Start",
                routeTemplate: "api/beserver/start",
                defaults: new { controller = "beserver", action = "start" }
            );

            config.Routes.MapHttpRoute(
                name: "Stop",
                routeTemplate: "api/beserver/stop",
                defaults: new { controller = "beserver", action = "stop" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.Use<WebApiLogUtil>();
            appBuilder.UseWebApi(config);
        }


    }


}
