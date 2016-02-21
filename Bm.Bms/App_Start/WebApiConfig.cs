using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Bm
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();


            //config.Routes.MapHttpRoute(
            //    name: "NoApi",
            //    routeTemplate: "api/No/{tbl}-{NoField}-{no}-for-{NameField}",
            //    defaults: new { NoField = "No", NameField = "Name" }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
