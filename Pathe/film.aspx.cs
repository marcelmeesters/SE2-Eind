using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe
{
    public partial class film : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string filmUrl = (string) Page.RouteData.Values["film"];
            string actionUrl = (string) Page.RouteData.Values["action"];
            string varsUrl = (string) Page.RouteData.Values["vars"];
            
            int filmID =
                int.Parse(filmUrl.Substring(0, filmUrl.IndexOf("-", StringComparison.Ordinal)));
        }
    }
}