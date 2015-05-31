using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe
{
    public partial class films : System.Web.UI.Page
    {
        private string listQuery = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Database db = Database.Instance;
            string action = (string)Page.RouteData.Values["action"];

            switch (action)
            {
                default:
                    listQuery =
                        "SELECT * FROM FILM f " +
                        "WHERE f.FilmID IN ( " +
                            "SELECT p.Film FROM PLANNING p " +
                            "WHERE p.Tijd >= (SELECT SYSDATE FROM dual) " +
                            "AND p.Tijd <= ((SELECT SYSDATE FROM dual) + 14)" +
                        " ) " +
                        "ORDER BY f.Titel ASC";
                    break;
                case "archief":
                    listQuery =
                        "SELECT * FROM FILM f ORDER BY f.Titel ASC";
                    break;
                case "verwacht":
                    listQuery =
                        "SELECT * FROM FILM f " +
                        "WHERE NOT f.FilmID IN ( " +
                            "SELECT p.Film FROM PLANNING p " +
                        " ) AND f.releasedate > (SELECT SYSDATE FROM dual)" +
                        "ORDER BY f.Titel ASC";
                    break;
            }
            FilmsDataSource.SelectCommand = listQuery;
        }
    }
}