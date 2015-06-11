using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe.admin
{
    public partial class cinemaAddProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string name = Request.Form["name"];
            string address = Request.Form["address"];
            string city = Request.Form["city"];
            string open = Request.Form["open"];
            string lift = (Request.Form["lift"] == "True") ? "1" : "0";
            string toilet = (Request.Form["toilet"] == "True") ? "1" : "0";
            string ring = (Request.Form["ring"] == "True") ? "1" : "0";
            string imax = (Request.Form["imax"] == "True") ? "1" : "0";

            try
            {
                bool success = Database.Instance.CreateCinema(name, address, city, open, lift, toilet, ring, imax);
                Response.Write(success ? "success" : "Error");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            
        }
    }
}