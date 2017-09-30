using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NHLDraftSimulator
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["UserID"];
            if (cookie != null)
            {
                try
                {
                    Session["UserID"] = new Guid(cookie.Values["userID"]);
                }
                catch (System.FormatException ex)
                {

                    DraftUser.addNewUser(Response, cookie);
                }

            }
            else
            {
                DraftUser.addNewUser(Response, cookie);
            }
        }

        protected void btnNewDraft_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SelectYear.aspx");
        }
    }
}