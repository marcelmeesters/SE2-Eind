using System;
using System.Text.RegularExpressions;
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
        private readonly string[] sortBy = { "TITEL", "RELEASEDATE", "DUUR", "FILMID" };
        private readonly string[] sortModes = { "ASC", "DESC" };
        private const int itemsPerPage = 12;

        private int page = 1;
        private string sort;
        private string sortMode;
        private string action;

        protected void Page_Load(object sender, EventArgs e)
        {
            Database db = Database.Instance;
            sort = (string)Page.RouteData.Values["sort"] ?? "TITEL";
            sortMode = (string)Page.RouteData.Values["sortmode"] ?? "ASC";
            action = (string) Page.RouteData.Values["action"] ?? "actuuel";

            // Pagination
            if (Page.RouteData.Values["page"] != null)
            {
                page = int.Parse((string)Page.RouteData.Values["page"]);
            }

            string lowLimit = Convert.ToString(itemsPerPage * (page - 1) + 1);
            string highLimit = Convert.ToString(itemsPerPage * page);

            // Sorting
            if (!sortBy.Contains(sort.ToUpper())) sort = "TITEL";
            if (!sortModes.Contains(sortMode.ToUpper())) sortMode = "ASC";

            btnSortTitle.Attributes["href"] = string.Format("/Films/{0}/{1}/{2}/{3}", action, page, "TITLE", sortMode);
            btnSortDate.Attributes["href"] = string.Format("/Films/{0}/{1}/{2}/{3}", action, page, "RELEASEDATE", sortMode);
            btnSortDuration.Attributes["href"] = string.Format("/Films/{0}/{1}/{2}/{3}", action, page, "DUUR", sortMode);
            btnSortId.Attributes["href"] = string.Format("/Films/{0}/{1}/{2}/{3}", action, page, "FILMID", sortMode);
            btnAsc.Attributes["href"] = string.Format("/Films/{0}/{1}/{2}/{3}", action, page, sort, "ASC");
            btnDesc.Attributes["href"] = string.Format("/Films/{0}/{1}/{2}/{3}", action, page, sort, "DESC");

            switch (sort)
            {
                default:
                    btnSortTitle.Attributes["class"] = "btn btn-success btn-small";
                    break;
                case "RELEASEDATE":
                    btnSortDate.Attributes["class"] = "btn btn-success btn-small";
                    break;
                case "DUUR":
                    btnSortDuration.Attributes["class"] = "btn btn-success btn-small";
                    break;
                case "FILMID":
                    btnSortId.Attributes["class"] = "btn btn-success btn-small";
                    break;
            }

            switch (sortMode)
            {
                default:
                    btnAsc.Attributes["class"] = "btn btn-success btn-small";
                    break;
                case "DESC":
                    btnDesc.Attributes["class"] = "btn btn-success btn-small";
                    break;
            }

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
                        ") where rnum  >= " + lowLimit +
                        "ORDER BY " + sort + " " + sortMode;
                    break;
                case "archief":
                    listQuery =
                        "select * from " +
                            "( select a.*, ROWNUM rnum from (" +
                                "SELECT * FROM FILM f ORDER BY f.Titel ASC " +
                            ") a "+
                            "where ROWNUM <= " + highLimit +
                        ") where rnum  >= " + lowLimit +
                        "ORDER BY " + sort + " " + sortMode;
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
                        ") where rnum  >= " + lowLimit +
                        "ORDER BY " + sort + " " + sortMode;
                    break;
            }
            System.Diagnostics.Debug.WriteLine(listQuery);
            FilmsDataSource.SelectCommand = listQuery;
        }

        public static string FormatTitleUrl(object title)
        {
            
            string title1 = title as string;

            return (title1 == null) ? null : Regex.Replace(title1.Replace(" ", "-"), "[^a-zA-Z0-9-]+", "");
        }
    }
}