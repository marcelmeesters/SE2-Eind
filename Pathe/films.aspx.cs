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
            int page = 1;
            if (Page.RouteData.Values["page"] != null)
            {
                page = int.Parse((string) Page.RouteData.Values["page"]);
            }

            int itemsPerPage = 12;

            if (Page.RouteData.Values["vars"] != null)
            {
                itemsPerPage = int.Parse((string) Page.RouteData.Values["vars"]);
            }

            string lowLimit = Convert.ToString(itemsPerPage*(page - 1) + 1);
            string highLimit = Convert.ToString(itemsPerPage*page);


            switch (action)
            {
                default:
                    listQuery =
                        "select * from " + 
                            "( select a.*, ROWNUM rnum from (" +
                                "SELECT * FROM FILM f " +
                                "WHERE f.FilmID IN ( " +
                                    "SELECT p.Film FROM PLANNING p " +
                                    "WHERE p.Tijd >= (SELECT SYSDATE FROM dual) " +
                                    "AND p.Tijd <= ((SELECT SYSDATE FROM dual) + 14)" +
                                " ) " +
                                "ORDER BY f.Titel ASC " +
                            ") a " +
                            "where ROWNUM <= " + highLimit +
                        ") where rnum  >= " + lowLimit;
                    break;
                case "archief":
                    listQuery =
                        "select * from " +
                            "( select a.*, ROWNUM rnum from (" +
                                "SELECT * FROM FILM f ORDER BY f.Titel ASC " +
                            ") a "+
                            "where ROWNUM <= " + highLimit +
                        ") where rnum  >= " + lowLimit;
                    break;
                case "verwacht":
                    listQuery =
                        "select * from " +
                            "( select a.*, ROWNUM rnum from (" +
                                "SELECT * FROM FILM f " +
                                "WHERE NOT f.FilmID IN ( " +
                                    "SELECT p.Film FROM PLANNING p " +
                                " ) AND f.releasedate > (SELECT SYSDATE FROM dual)" +
                                "ORDER BY f.Titel ASC" +
                            ") a " +
                            "where ROWNUM <= " + highLimit +
                        ") where rnum  >= " + lowLimit;
                    break;
            }
            System.Diagnostics.Debug.WriteLine(listQuery);
            FilmsDataSource.SelectCommand = listQuery;
        }
    }
}