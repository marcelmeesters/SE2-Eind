using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe.admin
{
    public partial class film : System.Web.UI.Page
    {
        private Database db = Database.Instance;

        protected void Page_Load(object sender, EventArgs e)
        {
            string filmUrl = (string)Page.RouteData.Values["film"];
            string actionUrl = (string)Page.RouteData.Values["action"];

            if (filmUrl == null || actionUrl == null) Response.Redirect("/Admin/Films/List/");
            if (!Regex.IsMatch(filmUrl, @"[0-9]+-.*")) Response.Redirect("/Admin/Films/List/");
            
            int filmID =
                int.Parse(filmUrl.Substring(0, filmUrl.IndexOf("-", StringComparison.Ordinal)));

            Dictionary<string, object> filmInfo = db.GetFilmInfo(filmID)[0];

            string[] releaseDate = filmInfo["RELEASEDATE"].ToString().Split(' ')[0].Split('-');
            int releaseDay = Convert.ToInt32(releaseDate[0]);
            int releaseYear = Convert.ToInt32(releaseDate[2]);
            int releaseMonth = 1;

            switch (releaseDate[1])
            {
                case "JAN":
                    releaseMonth = 1;
                    break;
                case "FEB":
                    releaseMonth = 2;
                    break;
                case "MAR":
                    releaseMonth = 3;
                    break;
                case "APR":
                    releaseMonth = 4;
                    break;
                case "MAY":
                    releaseMonth = 5;
                    break;
                case "JUN":
                    releaseMonth = 6;
                    break;
                case "JUL":
                    releaseMonth = 7;
                    break;
                case "AUG":
                    releaseMonth = 8;
                    break;
                case "SEP":
                    releaseMonth = 9;
                    break;
                case "OCT":
                    releaseMonth = 10;
                    break;
                case "NOV":
                    releaseMonth = 11;
                    break;
                case "DEC":
                    releaseMonth = 12;
                    break;

            }

            Film thisFilm = new Film(
                Convert.ToInt32(filmInfo["FILMID"]),
                filmInfo["TITEL"].ToString(),
                filmInfo["BESCHRIJVING"].ToString(),
                Convert.ToInt32(filmInfo["DUUR"]),
                new DateTime(releaseYear, releaseMonth, releaseDay),
                filmInfo["KIJKWIJZER"].ToString()
                );

            lblDescription.Text = thisFilm.Description;
            lblTitle.Text = thisFilm.Title;
            lblRelease.Text = thisFilm.Release.ToShortDateString();


            lblKijkwijzer.Text = thisFilm.KijkwijzerString;
        }
    }
}