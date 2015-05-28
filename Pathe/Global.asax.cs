using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Pathe
{
    public class Global : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("filmsActueel", "Films/Actueel", "~/films.aspx");
            routes.MapPageRoute("filmsArchief", "Films/Archief", "~/films.aspx");
            routes.MapPageRoute("filmsVerwacht", "Films/Verwacht", "~/films.aspx");

            routes.MapPageRoute("filmDetails", "Film/{prettyurl}", "~/film.aspx", true, new RouteValueDictionary{ { "pretty-url", null } });
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}