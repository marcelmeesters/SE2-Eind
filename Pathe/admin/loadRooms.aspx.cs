using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe.admin
{
    public partial class loadRooms : System.Web.UI.Page
    {
        private int cinemaID;
        protected void Page_Load(object sender, EventArgs e)
        {
            cinemaID = Convert.ToInt32(Request.Form["cinemaID"]);

            List<Dictionary<string, object>> zalens = Database.Instance.GetRooms(cinemaID);

            List<Zaal> zalen = MakeRoomList(zalens);

            DisplayTable(zalen);
        }

        private List<Zaal> MakeRoomList(List<Dictionary<string, object>> input )
        {
            return
                input.Select(
                    zaal =>
                        new Zaal(Convert.ToInt32(zaal["ZAALID"]), Convert.ToInt32(zaal["ZAALNUMMER"]),
                            Convert.ToInt32(zaal["AANTALSTOELEN"]), Convert.ToInt32(zaal["BIOSCOOPID"]),
                            (string.Equals(zaal["CANIMAX"], "1")))).ToList();
        }

        private void DisplayTable(List<Zaal> zalen)
        {
            string written =
                zalen.Aggregate(
                    "<table class='table table-striped table-hover'><thead><tr>" +
                    "<th>#</th><th>Nummer</th><th>Aantal plaatsen</th><th>Imax</th><th>Acties</th>" + "</tr></thead>" +
                    "<tbody",
                    (current, zaal) =>
                        current +
                        string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>Acties</td></tr>",
                            zaal.Id, zaal.Number, zaal.ChairCount, (zaal.Imax) ? "Ja" : "Nee"));
            written += "</tbody></table>";
            written += "<input type='hidden' id='cinemaID' value='" + cinemaID + "'/>";

            Response.Write(written);
        }
    }
}