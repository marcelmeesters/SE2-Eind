using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pathe
{
    public partial class finish_registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Let's add the user to the database
            alWrong.Visible = false;

            if (Database.Instance.GetCustomerCount(Membership.GetUser().UserName) > 0)
            {
                Response.Redirect("/");
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int huisnr;
                if (!int.TryParse(numHuisnr.Value, out huisnr))
                {
                    throw new ArgumentException();
                }

                string fName = txtName.Value;
                string tussen = txtTussen.Value;
                string lName = txtLastname.Value;

                string address = txtAdres.Value;
                string postcode = txtPostcode.Value;

                bool newsletter = chkNewsletter.Checked;

                SimpleDate bdate = new SimpleDate(Convert.ToDateTime(datBorn.Value));

                var membershipUser = Membership.GetUser();
                if (membershipUser != null)
                {
                    User temp = new User(0, membershipUser.UserName, membershipUser.Email, fName, tussen, lName, address, huisnr, postcode,
                        bdate, newsletter);

                    temp.Register();
                }
                else
                {
                    Response.Redirect("/");
                }
            }
            catch (ArgumentException)
            {
                alWrong.Visible = true;
            }

        }
    }
}