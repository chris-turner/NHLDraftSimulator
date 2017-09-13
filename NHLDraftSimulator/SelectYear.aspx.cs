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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("select distinct DraftYear from Player", con);
            ddlYears.DataValueField = "DraftYear";
            ddlYears.DataSource = cmd.ExecuteReader();
            ddlYears.DataBind();
            con.Close();
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TeamSelector.aspx?draftYear="+ ddlYears.SelectedValue);
        }
    }
}