using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SS_DB_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "route1",
                routeTemplate: "api/{controller}/{action}/{id}/{id2}",
                defaults: new { id=RouteParameter.Optional,id2 = RouteParameter.Optional }

            );

            //config.Routes.MapHttpRoute(
            //    name: "route2",
            //    routeTemplate: "api/{controller}/{action}/{id}"
            //);

            //config.Routes.MapHttpRoute(
            //    name: "route3",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            //config.Routes.MapHttpRoute(
            //    name: "route1",
            //    routeTemplate: "api/{controller}/{par1}/{par2}"
            //);

        }
    }
}
