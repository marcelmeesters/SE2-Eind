using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe
{
    public partial class film : System.Web.UI.Page
    {
        private readonly Database db = Database.Instance;

        protected void Page_Load(object sender, EventArgs e)
        {
            string filmUrl = (string) Page.RouteData.Values["film"];
            string actionUrl = (string) Page.RouteData.Values["action"];
            string varsUrl = (string) Page.RouteData.Values["vars"];

            if (filmUrl == null || actionUrl == null) Response.Redirect("/Films/Actueel");

            if(!Regex.IsMatch(filmUrl, @"[0-9]+-.*")) Response.Redirect("/Films/Actueel");
            
            int filmID =
                int.Parse(filmUrl.Substring(0, filmUrl.IndexOf("-", StringComparison.Ordinal)));

            Dictionary<string, object> filmInfo = db.GetFilmInfo(filmID)[0];

            lblDescription.Text = filmInfo["BESCHRIJVING"].ToString();
            lblTitle.Text = filmInfo["TITEL"].ToString();
        }
    }
}