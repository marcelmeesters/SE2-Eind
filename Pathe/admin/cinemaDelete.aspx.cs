using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe.admin
{
    public partial class cinemaDelete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int bioscoopID = Convert.ToInt32(Request.Form["cinemaID"]);
            int confirm = Convert.ToInt32(Request.Form["confirm"]);

            if (confirm == 1)
            {
                Response.Write(Database.Instance.DeleteCinema(bioscoopID) ? "success" : "fail");
            }
            else
            {
                Response.Write("noConfirm");
            }
        }
    }
}