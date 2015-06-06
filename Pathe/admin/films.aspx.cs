using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe.admin
{
    public partial class films : System.Web.UI.Page
    {
        private string listQuery = "";
        private readonly string[] sortBy = {"TITEL", "RELEASEDATE", "DUUR", "FILMID"};
        private readonly string[] sortModes = {"ASC", "DESC"};
        private const int itemsPerPage = 20;

        private int page = 1;
        private string sort = "TITEL";
        private string sortMode = "ASC";

        protected void Page_Load(object sender, EventArgs e)
        {
            Database db = Database.Instance;
            sort = (string) Page.RouteData.Values["sort"] ?? "TITEL";
            sortMode = (string) Page.RouteData.Values["sortmode"] ?? "ASC";

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

            btnSortTitle.Attributes["href"] = string.Format("/Admin/Films/List/{0}/{1}/{2}", page, "TITLE", sortMode);
            btnSortDate.Attributes["href"] = string.Format("/Admin/Films/List/{0}/{1}/{2}", page, "RELEASEDATE", sortMode);
            btnSortDuration.Attributes["href"] = string.Format("/Admin/Films/List/{0}/{1}/{2}", page, "DUUR", sortMode);
            btnSortId.Attributes["href"] = string.Format("/Admin/Films/List/{0}/{1}/{2}", page, "FILMID", sortMode);
            btnAsc.Attributes["href"] = string.Format("/Admin/Films/List/{0}/{1}/{2}", page, sort, "ASC");
            btnDesc.Attributes["href"] = string.Format("/Admin/Films/List/{0}/{1}/{2}", page, sort, "DESC");

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


            // Build query

            listQuery =
                "select * from " +
                    "( select a.*, ROWNUM rnum from (" +
                        "SELECT * FROM FILM f ORDER BY f.Titel ASC " +
                    ") a " +
                    "where ROWNUM <= " + highLimit +
                ") where rnum  >= " + lowLimit +
                "ORDER BY " + sort + " " + sortMode;

            
            FilmsDataSource.SelectCommand = listQuery;
        }

        public static string FormatTitleUrl(object title)
        {

            string title1 = title as string;

            return (title1 == null) ? null : Regex.Replace(title1.Replace(" ", "-"), "[^a-zA-Z0-9-]+", "");
        }
    }
}