using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe.admin
{
    public partial class doRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Form["action"] == null || Request.Form["action"].Length == 0)
                    throw new Exception("Action must be defined");

                string action = Request.Form["action"].ToLower();

                switch (action)
                {
                    default:
                        throw new Exception("Invalid action");
                        break;
                    case "load":
                        LoadRooms();
                        WriteForm();
                        break;
                    case "info":
                        InfoRoom();
                        break;
                    case "edit":
                        UpdateRoom();
                        break;
                    case "add":
                        AddRoom();
                        break;
                }
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.Debug)
                {
                    Response.Write(ex.ToString());

                    foreach (var item in Request.Form)
                    {
                        Response.Write(string.Format("{0}: {1} <br>", item, Request.Form[item.ToString()]));
                    }
                }
                else
                {
                    Response.Write(ex.Message);
                }
            }
        }

        #region Load

        private void LoadRooms()
        {
            int cinemaID = Convert.ToInt32(Request.Form["cinemaID"]);

            List<Dictionary<string, object>> zalens = Database.Instance.GetRooms(cinemaID);

            List<Zaal> zalen = MakeRoomList(zalens);

            DisplayTable(zalen, cinemaID);
        }


        private List<Zaal> MakeRoomList(List<Dictionary<string, object>> input)
        {
            return
                input.Select(
                    zaal =>
                        new Zaal(Convert.ToInt32(zaal["ZAALID"]), Convert.ToInt32(zaal["ZAALNUMMER"]),
                            Convert.ToInt32(zaal["AANTALSTOELEN"]), Convert.ToInt32(zaal["BIOSCOOPID"]),
                            (Convert.ToInt32(zaal["CANIMAX"]) == 1))).ToList();
        }

        private void DisplayTable(List<Zaal> zalen, int cinemaID)
        {
            string written =
                zalen.Aggregate(
                    "<table class='table table-striped table-hover' id='roomTable'><thead><tr>" +
                    "<th>#</th><th>Nummer</th><th>Aantal plaatsen</th><th>Imax</th><th>Acties</th>" + "</tr></thead>" +
                    "<tbody",
                    (current, zaal) =>
                        current +
                        string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td><i class='mdi-editor-mode-edit' onclick='LoadEditRoom()'></i></td></tr>",
                            zaal.Id, zaal.Number, zaal.ChairCount, (zaal.Imax) ? "<i class='mdi-action-done'></i>" : " "));
            written += "</tbody></table>";
            written += "<input type='hidden' id='cinemaID' value='" + cinemaID + "'/>";

            Response.Write(written);
        }

        #endregion

        #region Info
        private void InfoRoom()
        {
            int roomId = (Request.Form["roomID"] != null && Request.Form["roomID"].Length > 0)
                    ? Convert.ToInt32(Request.Form["roomID"])
                    : 0;

            if (roomId == 0) throw new Exception("Er is geen kamer gevonden");

            Dictionary<string, object> roomInfo = Database.Instance.GetRoomInfo(roomId);

            Zaal temp = new Zaal(roomId, Convert.ToInt32(roomInfo["ZAALNUMMER"]),
                Convert.ToInt32(roomInfo["AANTALSTOELEN"]), Convert.ToInt32(roomInfo["BIOSCOOPID"]),
                (roomInfo["CANIMAX"].ToString() == "1"));

            Response.Write(temp.ToJSON());
        }

        #endregion

        #region Update

        private void UpdateRoom()
        {
            try
            {
                if (Request.Form["cinemaID"] == null || Request.Form["cinemaID"].Length == 0)
                    throw new Exception("Er moet een bioscoop opgegeven worden");
                if (Request.Form["number"] == null || Request.Form["number"].Length == 0)
                    throw new Exception("Er moet een zaalnummer ingevoerd worden");
                if (Request.Form["chairs"] == null || Request.Form["chairs"].Length == 0)
                    throw new Exception("Het aantal stoelen moet ingevoerd worden");
                if (Request.Form["roomID"] == null || Request.Form["roomID"].Length == 0 || Request.Form["roomID"] == "0")
                    throw new Exception("Geef een geldig zaalnummer op");


                int cinemaID = Convert.ToInt32(Request.Form["cinemaID"]);
                int number = Convert.ToInt32(Request.Form["number"]);
                int chairs = Convert.ToInt32(Request.Form["chairs"]);
                int roomID = Convert.ToInt32(Request.Form["roomID"]);

                bool imax = (Request.Form["imax"].ToUpper() == "TRUE");

                if (!Database.Instance.CinemaExists(cinemaID))
                    throw new Exception("De gegeven bioscoop bestaat niet");

                if (!Database.Instance.RoomExists(roomID))
                    throw new Exception("De gegeven zaal bestaat niet");

                if (number > 99) throw new Exception("Zaalnummer mag niet groter dan 99 zijn");
                if (number < 1) throw new Exception("Zaalnummer mag niet kleiner dan 1 zijn");

                if (chairs > 999) throw new Exception("Het aantal stoelen mag niet groter dan 999 zijn");
                if (chairs < 1) throw new Exception("Het aantal stoelen mag niet minder dan 1 zijn");

                Zaal tmp = new Zaal(roomID, number, chairs, cinemaID, imax);

                bool success = tmp.Update();

                if(success) Response.Write("success");

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PATHE.GEEN_DUBBELE_ZAAL"))
                    throw new Exception("Dit zaalnummer bestaat al voor deze bioscoop.");
                throw;
            }
        }

        #endregion

        #region Add

        private void AddRoom()
        {
            try
            {
                if (Request.Form["cinemaID"] == null || Request.Form["cinemaID"].Length == 0)
                    throw new Exception("Er moet een bioscoop opgegeven worden");
                if (Request.Form["number"] == null || Request.Form["number"].Length == 0)
                    throw new Exception("Er moet een zaalnummer ingevoerd worden");
                if (Request.Form["chairs"] == null || Request.Form["chairs"].Length == 0)
                    throw new Exception("Het aantal stoelen moet ingevoerd worden");


                int cinemaID = Convert.ToInt32(Request.Form["cinemaID"]);
                int number = Convert.ToInt32(Request.Form["number"]);
                int chairs = Convert.ToInt32(Request.Form["chairs"]);

                bool imax = (Request.Form["imax"].ToUpper() == "TRUE");

                if (!Database.Instance.CinemaExists(cinemaID))
                    throw new Exception("De gegeven bioscoop bestaat niet");

                if (number > 99) throw new Exception("Zaalnummer mag niet groter dan 99 zijn");
                if (number < 1) throw new Exception("Zaalnummer mag niet kleiner dan 1 zijn");

                if (chairs > 999) throw new Exception("Het aantal stoelen mag niet groter dan 999 zijn");
                if (chairs < 1) throw new Exception("Het aantal stoelen mag niet minder dan 1 zijn");

                Zaal tmp = new Zaal(0, number, chairs, cinemaID, imax);

                int id = tmp.Create();

                Response.Write(id != 0 ? "success" : "fail");

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PATHE.GEEN_DUBBELE_ZAAL"))
                    throw new Exception("Dit zaalnummer bestaat al voor deze bioscoop.");
                throw;
            }
        }

        #endregion

        #region Misc

        private void WriteForm()
        {
            string written =
                "<hr/>" +
                "<h4>Zaal toevoegen</h4>" +
                "<form>" +
                    "<input type='hidden' id='roomID' value='0'/>" +
                    "<div class='row'>" +
                        "<div class='col-lg-12'>" +
                            "<div class='form-group col-lg-4'>" +
                                "<input class='form-control floating-label' id='numNummer' type='number' placeholder='Zaalnummer' data-hint='Het zaalnummer, dient uniek te zijn' required/><br/>" +
                            "</div><div class='form-group col-lg-4 col-lg-offset-1'>" +
                                "<input class='form-control floating-label' id='numChairs' type='number' placeholder='Aantal stoelen' data-hint='Hoeveel stoelen zijn er in deze zaal?' required/><br/>" +
                            "</div><div class='form-group col-lg-2 col-lg-offset-1'>" +
                                "<label>" +
                                    "<input type='checkbox' id='chkImax' /> IMAX" +
                                "</label>" +
                            "</div>" +
                        "</div>" +
                    "</div>" +
                    "<div class='row'>" +
                        "<div class='col-lg-12'>" +
                            "<center>" +
                                "<span class='btn btn-success btn-lg' id='btnAddRoom' onclick='AddRoom()'>Zaal toevoegen</span>" +
                                "<span class='btn btn-primary btn-lg hidden' id='btnEditRoom' onclick='updateRoom()'>Zaal bewerken</span>" +
                            "</center>" +
                        "</div>" +
                    "</div>" +
                "</form>";

            Response.Write(written);
        }

        #endregion
    }
}