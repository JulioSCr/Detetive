using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Detetive
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Suspeito", action = "Listar", id = UrlParameter.Optional }
                //defaults: new { controller = "Anotacao", action = "Anotacao", id = UrlParameter.Optional }
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                //defaults: new { controller = "Sala", action = "Ingressar", id = UrlParameter.Optional }
                //defaults: new { controller = "Partida", action = "Jogar", id = UrlParameter.Optional }
                //defaults: new { controller = "Partida", action = "Rolardados", id = UrlParameter.Optional }
            );
        }
    }
}
