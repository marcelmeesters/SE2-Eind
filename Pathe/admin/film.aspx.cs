using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;

namespace Pathe.admin
{
    public partial class film : System.Web.UI.Page
    {
        private Database db = Database.Instance;
        private int filmID;

        protected void Page_Load(object sender, EventArgs e)
        {
            string filmUrl = (string)Page.RouteData.Values["film"];
            string actionUrl = (string)Page.RouteData.Values["action"];

            if (filmUrl == null || actionUrl == null) Response.Redirect("/Admin/Films/List/");
            if (!Regex.IsMatch(filmUrl, @"[0-9]+-.*")) Response.Redirect("/Admin/Films/List/");
            
            filmID =
                int.Parse(filmUrl.Substring(0, filmUrl.IndexOf("-", StringComparison.Ordinal)));

            if (!db.FilmExists(filmID)) Response.Redirect("/Admin/Films/List/");

            if (!Page.IsPostBack)
            {
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

                txtTitel.Value = thisFilm.Title;
                txtDescription.Value = thisFilm.Description;
                numDuur.Value = Convert.ToString(thisFilm.Duration);

                imgPoster1.Src = "/img/upload/" + thisFilm.FilmId + "/" + thisFilm.PrimaryImage;

                datRelease.Value = thisFilm.Release.ToString("yyyy-MM-dd");

                kw_Al.Checked = thisFilm.KijkWijzers.Contains(Kijkwijzer.Al);
                kw_zes.Checked = thisFilm.KijkWijzers.Contains(Kijkwijzer.Zes);
                kw_negen.Checked = thisFilm.KijkWijzers.Contains(Kijkwijzer.Negen);
                kw_twaalf.Checked = thisFilm.KijkWijzers.Contains(Kijkwijzer.Twaalf);
                kw_zestien.Checked = thisFilm.KijkWijzers.Contains(Kijkwijzer.Zestien);
                kw_geweld.Checked = thisFilm.KijkWijzers.Contains(Kijkwijzer.Geweld);
                kw_angst.Checked = thisFilm.KijkWijzers.Contains(Kijkwijzer.Angst);
                kw_seks.Checked = thisFilm.KijkWijzers.Contains(Kijkwijzer.Seks);
                kw_discriminatie.Checked = thisFilm.KijkWijzers.Contains(Kijkwijzer.Discriminatie);
                kw_drugsalcohol.Checked = thisFilm.KijkWijzers.Contains(Kijkwijzer.Drugsalcohol);
                kw_taalgebruik.Checked = thisFilm.KijkWijzers.Contains(Kijkwijzer.Taalgebruik);

                chkNormaal.Checked = thisFilm.IsNormaal;
                chkDried.Checked = thisFilm.Is3D;
                chkImax.Checked = thisFilm.IsImax;
                chkI3D.Checked = thisFilm.IsI3D;
            }
        }

        protected void btnEditFilm_OnClick(object sender, EventArgs e)
        {
            Database db = Database.Instance;
            string titel = txtTitel.Value;
            string description = txtDescription.Value;
            List<string> images = new List<string>();

            var dingetje = Page.Request.Form;

            DateTime releasedate = Convert.ToDateTime(datRelease.Value);
            int duur = Convert.ToInt32(numDuur.Value);
            List<Kijkwijzer> kwList = new List<Kijkwijzer>();

            if (kw_Al.Checked) kwList.Add(Kijkwijzer.Al);
            if (kw_zes.Checked) kwList.Add(Kijkwijzer.Zes);
            if (kw_negen.Checked) kwList.Add(Kijkwijzer.Negen);
            if (kw_twaalf.Checked) kwList.Add(Kijkwijzer.Twaalf);
            if (kw_zestien.Checked) kwList.Add(Kijkwijzer.Zestien);
            if (kw_geweld.Checked) kwList.Add(Kijkwijzer.Geweld);
            if (kw_angst.Checked) kwList.Add(Kijkwijzer.Angst);
            if (kw_seks.Checked) kwList.Add(Kijkwijzer.Seks);
            if (kw_discriminatie.Checked) kwList.Add(Kijkwijzer.Discriminatie);
            if (kw_drugsalcohol.Checked) kwList.Add(Kijkwijzer.Drugsalcohol);
            if (kw_taalgebruik.Checked) kwList.Add(Kijkwijzer.Taalgebruik);

            bool normaal = chkNormaal.Checked;
            bool dried = chkDried.Checked;
            bool imax = chkImax.Checked;
            bool i3d = chkI3D.Checked;


            Film temp = new Film(filmID, titel, description, duur, releasedate, kwList, normaal, dried, imax, i3d);
            temp.Update();
            try
            {
                if (imgPoster.PostedFile == null || imgPoster.PostedFile.ContentLength <= 0) return;
                Directory.CreateDirectory(Server.MapPath("~\\img\\upload\\") + filmID);
                var file = Request.Files[0];
                images.Add(file.FileName);

                if (!Regex.IsMatch(file.FileName, @"^(.*\.)((jpg)|(jpeg)|(png)|(gif))$")) return;
                string saveAs = Server.MapPath("~\\img\\upload\\") + filmID + "\\" + file.FileName;
                file.SaveAs(saveAs);

                temp.PrimaryImage = file.FileName;

                db.SetPrimaryImage(filmID, file.FileName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }


            /*lblStatus.Text = (newID == 0)
                ? string.Format(failString, "Oeps!",
                    "Er is iets fout gegaan bij het toevoegen van de film. Probeer het nogmaals.")
                : string.Format(successString, "Film toegevoegd",
                    "De film is toegevoegd. <strong><a href='/Admin/Film/" + temp.UrlString + "'> ID " + newID +
                    "</a></strong>");
             * */
        }
    }
}