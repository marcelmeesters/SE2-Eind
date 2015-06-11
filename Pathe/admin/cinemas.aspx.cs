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

        public string RoomCountClass(int bioscoopID)
        {
            int zaalCount = Database.Instance.GetRoomCount(bioscoopID);

            string returned = "mdi-image-";

            if (zaalCount == 0)
            {
                returned += "crop-square";
            }
            else if (zaalCount < 9)
            {
                returned += "filter-" + zaalCount;
            }
            else
            {
                returned += "filter-9-plus";
            }

            return returned;
        }

    }
}