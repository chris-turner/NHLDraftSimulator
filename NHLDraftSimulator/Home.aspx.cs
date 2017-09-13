using System;
using System.Collections.Generic;
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
            if (Session["UserID"] == null)
            {
                Session["UserID"] = Guid.NewGuid();
            }
        }

        protected void btnNewDraft_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SelectYear.aspx");
        }
    }
}