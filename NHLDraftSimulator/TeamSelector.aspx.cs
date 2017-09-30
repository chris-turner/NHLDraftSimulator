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
           String draftYear = Request.QueryString["dy"]; 

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("select TeamID, TeamName, ImageFileName from Team where BeginYear <=" + draftYear +
                " and (EndYear >=" + draftYear.Trim() +"or EndYear is null)", con);
            TeamListView.DataSource = cmd.ExecuteReader();
            TeamListView.DataBind();
            con.Close();
        }

        protected void teamImage_onclick(object sender, EventArgs e)
        {
            Session["DraftID"] = Guid.NewGuid();
            ImageButton btn = (ImageButton)(sender);
            string teamID = btn.CommandArgument;
            string draftYear = Request.QueryString["dy"];
            string draftName = Request.QueryString["dn"];

            if (draftName.Trim() == "" || draftName == null)
            {
                draftName = "MyDraft";
            }


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand  cmd = new SqlCommand("insert into Draft (DraftID, UserID, DraftName,DraftYear) values ('" + Session["DraftID"] +"','" + Session["UserID"] + "','" + draftName + "' ,'" + draftYear+ "')", con);
            cmd.ExecuteReader();
            con.Close();
            con.Open();
            cmd = new SqlCommand("insert into User_Team_Draft (TeamID, UserID, DraftID) values(" + teamID + ",'" + Session["UserID"] + "','" + Session["DraftID"] + "')", con);
            cmd.ExecuteReader();
            con.Close();
            Session["DraftYear"] = Request.QueryString["dy"];
            Response.Redirect("~/NHLDraftSim.aspx");

        }
    }
}