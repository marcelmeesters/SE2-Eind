using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe.admin
{
    public partial class cinemas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string FormatUrl(object title)
        {

            string title1 = title as string;

            return (title1 == null) ? null : Regex.Replace(title1.Replace(" ", "-"), "[^a-zA-Z0-9-]+", "");
        }

    }
}