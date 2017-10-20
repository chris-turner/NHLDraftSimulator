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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["NHLDraftDB"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand("select DP.Round, DP.PickInRound as 'Pick', T.TeamName as 'Team' , P.PlayerFName + ' ' + P.PlayerLName as 'Player' from DraftPick as DP inner join Player_Draft_Pick as PDP on PDP.DraftPickID = DP.DraftPickID " +
                "inner join Team as T on T.TeamID = DP.TeamID inner join Player as P on P.PlayerID = PDP.PlayerTakenID where PDP.DraftID = '" + Session["DraftID"].ToString().Trim() + "' order by DP.Round, DP.PickInRound", con);
            draftResultsGridView.DataSource = cmd.ExecuteReader();
            draftResultsGridView.DataBind();
            con.Close();

            draftResults.Visible = true;
        }
        
    }
}