using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace Pathe
{
    [ExcludeFromCodeCoverage]
    public class Global : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            // <3 routing
            // General routes
            routes.MapPageRoute("filmsOverview", "Films/{action}/{page}/{sort}/{sortmode}", "~/films.aspx", true,
                new RouteValueDictionary {{"action", "actueel"}, {"page", "1"}, {"sort", "titel"}, {"sortmode", "asc"}});

            routes.MapPageRoute("filmDetails", "Film/{film}/{action}/{*vars}", "~/film.aspx", true,
                new RouteValueDictionary {{"action", "show"}, {"film", "none"}},
                new RouteValueDictionary {{"film", "[0-9]+-[0-9a-z-]*"}});

            // User routes
            routes.MapPageRoute("userProfile", "User/Me", "~/profile.aspx");
            routes.MapPageRoute("userLogin", "Login", "~/login.aspx");
            routes.MapPageRoute("userCreate", "Register", "~/register.aspx");
            routes.MapPageRoute("userLogout", "Logout", "~/logout.aspx");


            // Admin routes
            routes.MapPageRoute("adminOverview", "Admin", "~/admin/default.aspx");

            routes.MapPageRoute("adminFilms", "Admin/Films/list/{page}/{sort}/{sortmode}", "~/admin/films.aspx", true,
                new RouteValueDictionary { { "page", "1" }, { "sort", "titel" }, { "sortmode", "asc" } });

            routes.MapPageRoute("adminFilmAdd", "Admin/Films/add/{*vars}", "~/admin/filmAdd.aspx",
                true);

            routes.MapPageRoute("adminFilm", "Admin/Film/{film}/{action}", "~/admin/film.aspx", true,
                new RouteValueDictionary { { "action", "show" }, { "film", "none" } });

            routes.MapPageRoute("adminCinemas", "Admin/Cinemas/List/{page}/{sort}/{sortmode}", "~/admin/cinemas.aspx", true,
                new RouteValueDictionary {{"page", "1"}, {"sort", "name"}, {"sortmode", "asc"}});

            routes.MapPageRoute("adminCinemaAdd", "Admin/Cinemas/Add", "~/admin/cinemaAdd.aspx");

            routes.MapPageRoute("adminCinema", "Admin/Cinema/{cinema}/{action}", "~/admin/cinema.aspx", true,
                new RouteValueDictionary {{"action", "show"}, {"cinema", "none"}});

            routes.MapPageRoute("adminUsers", "Admin/Users/{action}/{user}", "~/admin/users.aspx", true,
                new RouteValueDictionary {{"action", "list"}, {"user", "none"}});

            routes.MapPageRoute("adminAgenda", "Admin/Agenda/{action}/{entry}", "~/admin/agenda.aspx", true,
                new RouteValueDictionary { { "action", "list" }, { "entry", "none" } });
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
                new ScriptResourceDefinition
                {
                    Path = "~/js/jquery.min.js"
                }
            );
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