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
            // <3 routing
            // General routes
            routes.MapPageRoute("filmsOverview", "Films/{action}/{page}/{*vars}", "~/films.aspx", true,
                new RouteValueDictionary {{"action", "actueel"}, {"page", "1"}});

            routes.MapPageRoute("filmDetails", "Film/{film}/{action}/{*vars}", "~/film.aspx", true,
                new RouteValueDictionary {{"action", "show"}, {"film", "none"}});

            // User routes
            routes.MapPageRoute("userProfile", "User/Me", "~/profile.aspx");


            // Admin routes
            routes.MapPageRoute("adminOverview", "Admin", "~/admin/default.aspx");

            routes.MapPageRoute("adminFilms", "Admin/Films/{action}/{film}", "~/admin/films.aspx", true,
                new RouteValueDictionary {{"action", "list"}, {"film", "none"}});

            routes.MapPageRoute("adminCinema", "Admin/Cinema/{action}/{cinema}", "~/admin/cinemas.aspx", true,
                new RouteValueDictionary {{"action", "list"}, {"cinema", "none"}});

            routes.MapPageRoute("adminUsers", "Admin/Users/{action}/{user}", "~/admin/users.aspx", true,
                new RouteValueDictionary {{"action", "list"}, {"user", "none"}});

            routes.MapPageRoute("adminAgenda", "Admin/Agenda/{action}/{entry}", "~/admin/agenda.aspx", true,
                new RouteValueDictionary { { "action", "list" }, { "entry", "none" } });
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