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
    public partial class TeamSelector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (Session["DraftID"] == null)
            {
                Session["DraftID"] = Guid.NewGuid();
            }

           String draftYear = Request.QueryString["draftYear"]; 

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("select TeamID, TeamName, ImageFileName from Team where BeginYear <="+ draftYear+
                " and (EndYear >="+ draftYear+"or EndYear is null)", con);
            TeamImageGridView.DataSource = cmd.ExecuteReader();
            TeamImageGridView.DataBind();
            con.Close();
        }

        protected void btnChoose_onclick(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)(sender);
            string teamID = btn.CommandArgument;
            String draftYear = Request.QueryString["draftYear"];
            string draftName = "TempName"; //will need to be changed at some point to allow user to change name

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into [User] (UserID) values ('" + Session["UserID"] + "')", con);
            cmd.ExecuteReader();
            con.Close();
            con.Open();
            cmd = new SqlCommand("insert into Draft (DraftID, UserID, DraftName,DraftYear) values ('" + Session["DraftID"] +"','" + Session["UserID"] + "','" + draftName + "' ,'" + draftYear+ "')", con);
            cmd.ExecuteReader();
            con.Close();
            con.Open();
            cmd = new SqlCommand("insert into User_Team_Draft (TeamID, UserID, DraftID) values(" + teamID + ",'" + Session["UserID"] + "','" + Session["DraftID"] + "')", con);
            cmd.ExecuteReader();
            con.Close();
            Session["DraftYear"] = Request.QueryString["draftYear"];
            Response.Redirect("~/NHLDraftSim.aspx");

        }
    }
}