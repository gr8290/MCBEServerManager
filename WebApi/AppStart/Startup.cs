using Owin;
using System.Web.Http;
using WebApi.Handler;
using WebApi.Util;

namespace WebApi.AppStart
{
    class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for Self-Host
            HttpConfiguration config = new HttpConfiguration();

            //config.Routes.MapHttpRoute(
            //    name: "Start",
            //    routeTemplate: "api/beserver/start",
            //    defaults: new { controller = "beserver", action = "start" }
            //);
            //config.Routes.MapHttpRoute(
            //    name: "Stop",
            //    routeTemplate: "api/beserver/stop",
            //    defaults: new { controller = "beserver", action = "stop" }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "Status",
            //    routeTemplate: "api/beserver/status",
            //    defaults: new { controller = "beserver", action = "status" }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.MessageHandlers.Add(new ErrorMessageHandler());
            appBuilder.Use<WebApiLogUtil>();
            appBuilder.UseWebApi(config);
        }
    }
}
