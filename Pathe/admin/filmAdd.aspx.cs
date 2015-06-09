using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe.admin
{
    public partial class filmAdd : System.Web.UI.Page
    {
        private string successString =
            "<div class='alert alert-dismissable alert-success'>" +
                "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                "<strong>{0}</strong> {1}</a>." +
            "</div>";

        private string failString =
            "<div class='alert alert-dismissable alert-danger'>" +
                "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                "<strong>{0}</strong> {1}" +
            "</div>";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnAddFilm_OnClick(object sender, EventArgs e)
        {
            Database db = Database.Instance;
            string titel = txtTitel.Value;
            string description = txtDescription.Value;
            List<string> images = new List<string>();

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


            Film temp = new Film(0, titel, description, duur, releasedate, kwList);
            int newID = temp.Create();

            try
            {
                Directory.CreateDirectory(Server.MapPath("~\\img\\upload\\") + newID);

                if (imgPoster.PostedFile != null && imgPoster.PostedFile.ContentLength > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var file = Request.Files[i];
                        images.Add(file.FileName);

                        if (!Regex.IsMatch(file.FileName, @"^(.*\.)((jpg)|(jpeg)|(png)|(gif))$")) continue;

                        string saveAs = Server.MapPath("~\\img\\upload\\") + newID + "\\" + file.FileName;
                        file.SaveAs(saveAs);
                    }
                    if (!db.AddFilmImages(newID, images))
                    {
                        throw new Exception("Could not insert uploaded images into database.");
                    }
                    temp.Images = images;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            lblStatus.Text = (newID == 0)
                ? string.Format(failString, "Oeps!",
                    "Er is iets fout gegaan bij het toevoegen van de film. Probeer het nogmaals.")
                : string.Format(successString, "Film toegevoegd",
                    "De film is toegevoegd. <strong><a href='/Admin/Film/" + temp.UrlString + "'> ID " + newID +
                    "</a></strong>");
        }
    }
}