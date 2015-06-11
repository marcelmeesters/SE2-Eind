using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe.admin
{
    public partial class addRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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

                bool imax = (Request.Form["imax"] == "True");

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
                Response.Write(ex.Message.Contains("PATHE.GEEN_DUBBELE_ZAAL")
                    ? "Dit zaalnummer bestaat al voor deze bioscoop."
                    : ex.Message);
            }
        }
    }
}