using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe
{
    public partial class roles : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Roles.RoleExists("GlobalAdmin")) Roles.CreateRole("GlobalAdmin");
            if (!Roles.RoleExists("FilmAdmin")) Roles.CreateRole("FilmAdmin");
            if (!Roles.RoleExists("BlogAdmin")) Roles.CreateRole("BlogAdmin");
            if (!Roles.RoleExists("UserAdmin")) Roles.CreateRole("UserAdmin");
            if (!Roles.RoleExists("Customer")) Roles.CreateRole("Customer");

            
        }
    }
}