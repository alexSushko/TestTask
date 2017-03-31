using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing.Constraints;
namespace PetsOwners
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "OwnerSearchParamRoute",
                routeTemplate: "api/{controller}/{action}/{name}/{page}/{items}/{filter}",
                defaults: new { page = 1, items = 3, filter = "Id-Down" },
                constraints: new
                {
                    name = new AlphaRouteConstraint()
                }
            );
            config.Routes.MapHttpRoute(
                name: "OwnerGetParamRoute",
                routeTemplate: "api/{controller}/{action}/{page}/{items}/{filter}",
                defaults: new { page = 1, items = 3, filter = "Id-Down" }
            
            );
            config.Routes.MapHttpRoute(
                name: "PetSearchParamRoute",
                routeTemplate: "api/{controller}/{action}/{ownerId}/{name}/{page}/{items}/{filter}",
                defaults: new {page = 1, items = 3, filter = "Id-Down" },
                constraints: new
                {
                    name = new AlphaRouteConstraint(),
                    ownerId=new LongRouteConstraint()
                }
            );
            config.Routes.MapHttpRoute(
                name: "PetGetParamRoute",
                routeTemplate: "api/{controller}/{action}/{ownerId}/{page}/{items}/{filter}",
                defaults: new { page = 1, items = 3, filter = "Id-Down" },
                constraints: new
                {
                    ownerId = new LongRouteConstraint()
                }

            );
           
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
