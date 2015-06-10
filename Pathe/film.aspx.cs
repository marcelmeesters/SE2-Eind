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
        private Database db = Database.Instance;

        protected void Page_Load(object sender, EventArgs e)
        {
            string filmUrl = (string)Page.RouteData.Values["film"];
            string actionUrl = (string)Page.RouteData.Values["action"];

            if (filmUrl == null || actionUrl == null) Response.Redirect("/Films/");
            if (!Regex.IsMatch(filmUrl, @"[0-9]+-.*")) Response.Redirect("/Films/");

            int filmID =
                int.Parse(filmUrl.Substring(0, filmUrl.IndexOf("-", StringComparison.Ordinal)));

            Dictionary<string, object> filmInfo = db.GetFilmInfo(filmID)[0];

            SimpleDate simDate = new SimpleDate(filmInfo["RELEASEDATE"].ToString());

            DateTime releaseDate = simDate.RealDate;

            Film thisFilm = new Film(
                Convert.ToInt32(filmInfo["FILMID"]),
                filmInfo["TITEL"].ToString(),
                filmInfo["BESCHRIJVING"].ToString(),
                Convert.ToInt32(filmInfo["DUUR"]),
                releaseDate,
                filmInfo["KIJKWIJZER"].ToString(),
                (filmInfo["ISNORMAAL"].ToString() == "1"),
                (filmInfo["ISDRIED"].ToString() == "1"),
                (filmInfo["ISIMAX"].ToString() == "1"),
                (filmInfo["ISI3D"].ToString() == "1"),
                filmInfo["AFBEELDING"].ToString()
                );

            lblDescription.Text = thisFilm.Description;
            lblTitle.Text = thisFilm.Title;
            lblRelease.Text = thisFilm.Release.ToString("dd MMM yyyy");
            imgPoster.Src = "/img/upload/" + thisFilm.FilmId + "/" + thisFilm.PrimaryImage;

            lblKijkwijzer.Text = thisFilm.KijkwijzerStringImg;
        }
    }
}